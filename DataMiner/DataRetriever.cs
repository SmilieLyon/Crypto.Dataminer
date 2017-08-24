using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using System.Linq;
using Coinigy.API;
using Coinigy.API.Responses;
using DevelopmentHelperLib.Core;
using Domain.Base;
using Domain.Dataminer;
using Domain.Dataminer.Entities;
using Newtonsoft.Json;
using Exchange = Coinigy.API.Responses.Exchange;
using Market = Coinigy.API.Responses.Market;

namespace DataMiner
{
    public class ExchangeMarketDataRetriever
    {
        public ExchangeMarketDataRetriever(string apikey, string apisecret, StorageAppender appender)
        {
            Appender = appender;
            Api = new CoinigyApi(apikey, apisecret, "https://api.coinigy.com/api/v1/");
        }

        private CoinigyApi Api { get; }
        private StorageAppender Appender { get; }

        private FileStore<IList<ExchangeMarkets>> Markets { get; set; }

        private void ListMarkets()
        {
            if (Markets == null && Appender.HasData)
            {
                Markets = JsonConvert.DeserializeObject<FileStore<IList<ExchangeMarkets>>>(Appender.ReadAllText());
            }
            else if (Markets != null && Markets.LastSaved.Add(Markets.Expires) > DateTime.Now)
            {
                return;
            }
            IList<ExchangeMarkets> exchangeMarkets;

            if (File.Exists("exchanges.json"))
            {
                exchangeMarkets =
                    JsonConvert.DeserializeObject<IList<ExchangeMarkets>>(File.ReadAllText("exchanges.json"));
            }
            else
            {
                var exchanges = Api.Exchanges();

                exchangeMarkets = Mapper.Map<IList<ExchangeMarkets>>(exchanges.data);

                foreach (var exchange in exchangeMarkets)
                {
                    exchange.Markets = Mapper.Map<IList<MarketValue>>(Api.Markets(exchange.exch_code).data);
                }

                File.WriteAllText("exchanges.json", JsonConvert.SerializeObject(exchangeMarkets));
            }
            SaveExchangeMarketList(exchangeMarkets);
        }

        private void SaveExchangeMarketList(IList<ExchangeMarkets> exchangeMarkets)
        {
            using (var dbContext = DataMinerContext.Create())
            {
                var exchangeRepo = dbContext.CreateRepository<Domain.Dataminer.Entities.Exchange>();
                var apiRepo = dbContext.CreateRepository<Api>();
                var apiExchangeRepo = dbContext.CreateRepository<ApiExchange>();
                var apiMarketRepo = dbContext.CreateRepository<ApiMarket>();
                var marketRepo = dbContext.CreateRepository<Domain.Dataminer.Entities.Market>();
                var assetRepo = dbContext.CreateRepository<Asset>();
                var apiAssetRepo = dbContext.CreateRepository<ApiAsset>();

                var coinigyApi = GetCreateCoinigyApi(apiRepo);

                foreach (var exchangeMarket in exchangeMarkets)
                {
                    var exchangeApi = CheckExchangeApiExistsInDb(exchangeRepo, apiExchangeRepo, coinigyApi, exchangeMarket);
                    foreach (var market in exchangeMarket.Markets)
                    {
                        var marketApi = CheckMarketApiExistsInDb(apiMarketRepo, marketRepo, apiAssetRepo, assetRepo, exchangeApi, market);
                    }
                }
            }
        }

        private ApiMarket CheckMarketApiExistsInDb(IGenericRepository<ApiMarket> apiMarketRepo, IGenericRepository<Domain.Dataminer.Entities.Market> marketRepo, IGenericRepository<ApiAsset> apiAssetRepo, IGenericRepository<Asset> assetRepo, ApiExchange apiExchange, MarketValue marketValue)
        {
            var market = CheckMarketExistsInDb(marketRepo, apiAssetRepo, assetRepo, apiExchange, marketValue);
            var apiMarket = apiMarketRepo.Query().FirstOrDefault(x => x.MarketId == market.MarketId && x.ApiId == apiExchange.ApiId);
            if (apiMarket == null)
            {
                apiMarket = new ApiMarket
                {
                    Api = apiExchange.Api,
                    Market = market,
                    Code = marketValue.mkt_name,
                    ExternalName = marketValue.mkt_id
                };
                apiMarketRepo.Insert(apiMarket);
                apiMarketRepo.SaveChanges();
            }
            return apiMarket;
        }

        private Domain.Dataminer.Entities.Market CheckMarketExistsInDb(IGenericRepository<Domain.Dataminer.Entities.Market> marketRepo, IGenericRepository<ApiAsset> apiAssetRepo, IGenericRepository<Asset> assetRepo, ApiExchange apiExchange, MarketValue marketValue)
        {
            var marketNames = marketValue.mkt_name.Split('/');
            var marketTo = CheckAssetApiExistsInDb(apiAssetRepo, assetRepo, apiExchange, marketNames[0]);
            var marketFrom = CheckAssetApiExistsInDb(apiAssetRepo, assetRepo, apiExchange, marketNames[1]);
            var market = marketRepo.Query().FirstOrDefault(x => x.PrimaryAssetId == marketTo.AssetId && x.SecondaryAssetId == marketFrom.AssetId);
            if (market == null)
            {
                market = new Domain.Dataminer.Entities.Market
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

        private ApiAsset CheckAssetApiExistsInDb(IGenericRepository<ApiAsset> apiAssetRepo, IGenericRepository<Asset> assetRepo, ApiExchange apiExchange, string marketName)
        {
            var asset = CheckAssetExistsInDb(assetRepo, apiExchange, marketName);
            var assetApi = apiAssetRepo.Query().FirstOrDefault(x => x.AssetId == asset.AssetId);
            if (assetApi == null)
            {
                assetApi = new ApiAsset
                {
                    Api = apiExchange.Api,
                    Code = marketName,
                    Asset = asset,
                };
                apiAssetRepo.Insert(assetApi);
                apiAssetRepo.SaveChanges();
            }
            return assetApi;
        }

        private Asset CheckAssetExistsInDb(IGenericRepository<Asset> assetRepo, ApiExchange apiExchange, string assetName)
        {
            var market = assetRepo.Query().FirstOrDefault(x => x.Name == assetName);
            if (market == null)
            {
                market = new Asset { 
                    Name = assetName,
                    Description = assetName
                };
                assetRepo.Insert(market);
                assetRepo.SaveChanges();
            }
            return market;
        }

        private ApiExchange CheckExchangeApiExistsInDb(IGenericRepository<Domain.Dataminer.Entities.Exchange> exchangeRepo, IGenericRepository<ApiExchange> apiExchangeRepo, Api coinigyApi, ExchangeMarkets exchangeMarket)
        {
            var exchange = CheckExchangeExistsInDb(exchangeRepo, exchangeMarket);
            var api = apiExchangeRepo.Query().FirstOrDefault(x => x.ExchangeId == exchange.ExchangeId && x.ApiId == coinigyApi.ApiId);
            if (api == null)
            {
                api = new ApiExchange
                {
                    Api = coinigyApi,
                    Exchange = exchange,
                    BalanceEnabled = exchangeMarket.exch_balance_enabled.ParseToBool(),
                    TradeEnabled = exchangeMarket.exch_trade_enabled.ParseToBool(),
                    Code = exchangeMarket.exch_code,
                    ExternalId = exchangeMarket.exch_id,
                    Fee = exchangeMarket.exch_fee.ParseToDecimal()
                };
                apiExchangeRepo.Insert(api);
                apiExchangeRepo.SaveChanges();
            }
            return api;
        }

        private Domain.Dataminer.Entities.Exchange CheckExchangeExistsInDb(IGenericRepository<Domain.Dataminer.Entities.Exchange> exchangeRepo, ExchangeMarkets exchangeMarket)
        {
            var api = exchangeRepo.Query().FirstOrDefault(x => x.Name == exchangeMarket.exch_name);
            if (api == null)
            {
                api = new Domain.Dataminer.Entities.Exchange
                {
                    Name = exchangeMarket.exch_name,
                    Url = exchangeMarket.exch_url
                };
                exchangeRepo.Insert(api);
                exchangeRepo.SaveChanges();
            }
            return api;
        }

        private Api GetCreateCoinigyApi(IGenericRepository<Api> apiRepo)
        {
            var api = apiRepo.Query().FirstOrDefault(x => x.Name == "Coinigy");
            if (api == null)
            {
                api = new Api
                {
                    Name = "Coinigy"
                };
                apiRepo.Insert(api);
                apiRepo.SaveChanges();
            }
            return api;
        }

        public ticker_response GetTick(string exchange, string code)
        {
            //var data = Api.Data("PLNX", "BTC/USDT", MarketDataType.history);
            ListMarkets();
            return Api.Ticker(exchange, code);
        }

        public class ExchangeMarkets : Exchange
        {
            public IList<MarketValue> Markets { get; set; }
        }

        public class MarketValue : Market
        {
            public decimal Volume { get; set; }
        }
    }
}