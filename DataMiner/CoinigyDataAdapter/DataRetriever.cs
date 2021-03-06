﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using AutoMapper;
using Coinigy.API;
using Coinigy.API.Responses;
using DevelopmentHelperLib.Core;
using Domain.Base;
using Domain.Dataminer;
using Domain.Dataminer.Entities;
using Domain.Dataminer.Extensions;
using Exchange = Coinigy.API.Responses.Exchange;
using Market = Domain.Dataminer.Entities.Market;

namespace DataMiner.CoinigyDataAdapter
{
    public class ExchangeMarketDataRetriever : IMarketLister
    {
        private static readonly TimeSpan _callSeparation = TimeSpan.FromMilliseconds(330);
        private CoinigyApi _api;
        private DateTime _timeSinceLastApiCall;

        public ExchangeMarketDataRetriever(string apikey, string apisecret)
        {
            Api = new CoinigyApi(apikey, apisecret, "https://api.coinigy.com/api/v1/");
        }

        private CoinigyApi Api
        {
            get
            {
                // Coinigy has max 3 calls per second
                if (_timeSinceLastApiCall == null)
                {
                    _timeSinceLastApiCall = DateTime.Now;
                    return _api;
                }
                var timeSince = DateTime.Now - _timeSinceLastApiCall;
                if (timeSince < _callSeparation)
                {
                    Thread.Sleep(_callSeparation - timeSince);
                }
                _timeSinceLastApiCall = DateTime.Now;
                return _api;
            }
            set => _api = value;
        }

        public void UpdateMarketList()
        {
            using (var dbContext = DataMinerContext.Create())
            {
                var apiRepo = dbContext.CreateRepository<Api>();
                var coinigyApi = apiRepo.GetCreateApi("Coinigy");

                if (coinigyApi.LastUpdated != null && coinigyApi.LastUpdated > DateTime.Now.AddDays(-7))
                {
                    return;
                }

                IList<ExchangeMarkets> exchangeMarkets;
                var exchanges = Api.Exchanges();

                exchangeMarkets = Mapper.Map<IList<ExchangeMarkets>>(exchanges.data);

                foreach (var exchange in exchangeMarkets)
                {
                    exchange.Markets = Mapper.Map<IList<MarketValue>>(Api.Markets(exchange.exch_code).data);
                }

                var bitMex = exchangeMarkets.FirstOrDefault(exch => exch.exch_code == "BMEX");
                if (bitMex != null)
                {
                    UpdateMarketName(bitMex, "BLOCKSB_Z17", "BLOCKS/B_Z17");
                    UpdateMarketName(bitMex, "DASHU17", "DASH/U17");
                    UpdateMarketName(bitMex, "EOSN17", "EOS/N17");
                    UpdateMarketName(bitMex, "ETC7D", "ETC/7D");
                    UpdateMarketName(bitMex, "ETHU17", "ETH/U17");
                    UpdateMarketName(bitMex, "LTCU17", "LTC/U17");
                    UpdateMarketName(bitMex, "QTUMU17", "QTUM/U17");
                    UpdateMarketName(bitMex, "SEGWITB_Z17", "SEGWIT/B_Z17");
                    UpdateMarketName(bitMex, "SNTN17", "SNT/N17");
                    UpdateMarketName(bitMex, "XBJU17", "XBJ/U17");
                    UpdateMarketName(bitMex, "XBTU17", "XBT/U17");
                    UpdateMarketName(bitMex, "XBTUSD", "XBT/USD");
                    UpdateMarketName(bitMex, "XMRU17", "XMR/U17");
                    UpdateMarketName(bitMex, "XRPU17", "XRP/U17");
                    UpdateMarketName(bitMex, "XTZZ17", "XTZ/Z17");
                    UpdateMarketName(bitMex, "ZECU17", "ZEC/U17");
                }
                SaveExchangeMarketList(exchangeMarkets, dbContext, coinigyApi);
            }
        }

        private void UpdateMarketName(ExchangeMarkets bitMex, string v1, string v2)
        {
            var obj = bitMex.Markets.FirstOrDefault(mkt => mkt.mkt_name == v1);
            if (obj == null)
            {
                return;
            }
            obj.mkt_name = v2;
        }

        private void SaveExchangeMarketList(IList<ExchangeMarkets> exchangeMarkets, IDataMinerContext dbContext,
            Api coinigyApi)
        {
            var exchangeRepo = dbContext.CreateRepository<Domain.Dataminer.Entities.Exchange>();
            var apiRepo = dbContext.CreateRepository<Api>();
            var apiExchangeRepo = dbContext.CreateRepository<ApiExchange>();
            var apiMarketRepo = dbContext.CreateRepository<ApiMarket>();
            var marketRepo = dbContext.CreateRepository<Market>();
            var assetRepo = dbContext.CreateRepository<Asset>();
            var apiAssetRepo = dbContext.CreateRepository<ApiAsset>();

            var apiExchanges = apiExchangeRepo.Query().Where(x => x.ApiId == coinigyApi.ApiId);
            foreach (var exchange in apiExchanges)
            {
                exchange.Enabled = (exchangeMarkets.Any(x => x.exch_code == exchange.Code));
            }

            foreach (var exchangeMarket in exchangeMarkets)
            {
                var exchange = exchangeRepo.CheckExchangeExistsInDb(exchangeMarket.exch_name, exchangeMarket.exch_url);
                var exchangeApi = apiExchangeRepo.CheckExchangeApiExistsInDb(
                    coinigyApi,
                    exchange,
                    exchangeMarket.exch_balance_enabled.ParseToBool(),
                    exchangeMarket.exch_trade_enabled.ParseToBool(),
                    exchangeMarket.exch_code,
                    exchangeMarket.exch_id,
                    exchangeMarket.exch_fee.ParseToDecimal());

                foreach (var market in exchangeMarket.Markets)
                {
                    var marketApi = CheckMarketApiExistsInDb(apiMarketRepo, marketRepo, apiAssetRepo, assetRepo,
                        exchangeApi, market);
                }
                var dbExchangeMarkets = apiMarketRepo.Query()
                    .Include(x => x.Market)
                    .Where(market => market.ApiId == coinigyApi.ApiId &&
                                     market.Market.ExchangeId == exchangeApi.ExchangeId);

                foreach (var mkt in dbExchangeMarkets)
                {
                    mkt.Enabled = exchangeMarket.Markets.Any(market1 => market1.mkt_name == mkt.Code);
                }
                exchangeApi.PairsLastUpdated = DateTime.Now;
            }
            coinigyApi.LastUpdated = DateTime.Now;
            dbContext.SaveChanges();
        }

        private ApiMarket CheckMarketApiExistsInDb(IGenericRepository<ApiMarket> apiMarketRepo,
            IGenericRepository<Market> marketRepo, IGenericRepository<ApiAsset> apiAssetRepo,
            IGenericRepository<Asset> assetRepo, ApiExchange apiExchange, MarketValue marketValue)
        {
            var market = CheckMarketExistsInDb(marketRepo, apiAssetRepo, assetRepo, apiExchange, marketValue);
            var apiMarket = apiMarketRepo.Query()
                .FirstOrDefault(x => x.MarketId == market.MarketId && x.ApiId == apiExchange.ApiId);
            if (apiMarket == null)
            {
                apiMarket = new ApiMarket
                {
                    Api = apiExchange.Api,
                    Market = market,
                    Code = marketValue.mkt_name,
                    ExternalId = marketValue.mkt_id
                };
                apiMarketRepo.Insert(apiMarket);
                apiMarketRepo.SaveChanges();
            }
            return apiMarket;
        }

        private Market CheckMarketExistsInDb(IGenericRepository<Market> marketRepo,
            IGenericRepository<ApiAsset> apiAssetRepo, IGenericRepository<Asset> assetRepo, ApiExchange apiExchange,
            MarketValue marketValue)
        {
            var marketNames = marketValue.mkt_name.Split('/');
            var marketTo = CheckAssetApiExistsInDb(apiAssetRepo, assetRepo, apiExchange, marketNames[0]);
            var marketFrom = CheckAssetApiExistsInDb(apiAssetRepo, assetRepo, apiExchange, marketNames[1]);
            var market = marketRepo.Query()
                .FirstOrDefault(x => x.PrimaryAssetId == marketTo.AssetId && x.SecondaryAssetId == marketFrom.AssetId);
            if (market == null)
            {
                market = new Market
                {
                    Exchange = apiExchange.Exchange,
                    Name = marketValue.mkt_name,
                    PrimaryAsset = marketTo.Asset,
                    SecondaryAsset = marketFrom.Asset
                };
                marketRepo.Insert(market);
                marketRepo.SaveChanges();
            }
            return market;
        }

        private ApiAsset CheckAssetApiExistsInDb(IGenericRepository<ApiAsset> apiAssetRepo,
            IGenericRepository<Asset> assetRepo, ApiExchange apiExchange, string marketName)
        {
            var asset = assetRepo.CheckAssetExistsInDb(marketName);
            var assetApi = apiAssetRepo.Query().FirstOrDefault(x => x.AssetId == asset.AssetId);
            if (assetApi == null)
            {
                assetApi = new ApiAsset
                {
                    Api = apiExchange.Api,
                    Code = marketName,
                    Asset = asset
                };
                apiAssetRepo.Insert(assetApi);
                apiAssetRepo.SaveChanges();
            }
            return assetApi;
        }

        public ticker_response GetTick(string exchange, string code)
        {
            //var data = Api.Data("PLNX", "BTC/USDT", MarketDataType.history);
            return Api.Ticker(exchange, code);
        }

        public class ExchangeMarkets : Exchange
        {
            public IList<MarketValue> Markets { get; set; }
        }

        public class MarketValue : Coinigy.API.Responses.Market
        {
            public decimal Volume { get; set; }
        }
    }
}