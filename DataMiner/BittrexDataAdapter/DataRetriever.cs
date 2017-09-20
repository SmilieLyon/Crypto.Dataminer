using System;
using System.Linq;
using Bittrex.Api;
using Bittrex.Models;
using Domain.Base;
using Domain.Dataminer;
using Domain.Dataminer.Entities;
using Domain.Dataminer.Extensions;

namespace DataMiner.BittrexDataAdapter
{
    public interface IHistoryTicker
    {
    }

    public class ExchangeMarketDataRetriever : IMarketLister, IHistoryTicker
    {
        public ExchangeMarketDataRetriever(string apikey, string apisecret)
        {
        }

        public void UpdateMarketList()
        {
            var marketAsync = PublicApi.GetMarketsAsync();
            marketAsync.Wait();
            var markets = marketAsync.Result;

            using (var dbContext = DataMinerContext.Create())
            {
                var apiRepo = dbContext.CreateRepository<Api>();
                var exchangeRepo = dbContext.CreateRepository<Exchange>();
                var apiExchangeRepo = dbContext.CreateRepository<ApiExchange>();
                var apiMarketRepo = dbContext.CreateRepository<ApiMarket>();
                var marketRepo = dbContext.CreateRepository<Market>();
                var assetRepo = dbContext.CreateRepository<Asset>();
                var apiAssetRepo = dbContext.CreateRepository<ApiAsset>();

                var bittrexApi = apiRepo.GetCreateApi("Bittrex");

                if (bittrexApi.LastUpdated != null && bittrexApi.LastUpdated > DateTime.Now.AddDays(-7))
                {
                    return;
                }

                var exchange = exchangeRepo.CheckExchangeExistsInDb("Bittrex", "https://bittrex.com/");

                var exchangeApi = apiExchangeRepo.CheckExchangeApiExistsInDb(
                    bittrexApi,
                    exchange,
                    true,
                    true,
                    "BTRX",
                    "15",
                    0.0025m);

                markets = markets.Where(mark => mark.IsActive).ToList();

                foreach (var market in markets)
                {
                    var marketApi = CheckMarketApiExistsInDb(apiMarketRepo, marketRepo, apiAssetRepo, assetRepo,
                        exchangeApi, market);
                }
            }
        }

        private ApiMarket CheckMarketApiExistsInDb(IGenericRepository<ApiMarket> apiMarketRepo,
            IGenericRepository<Market> marketRepo, IGenericRepository<ApiAsset> apiAssetRepo,
            IGenericRepository<Asset> assetRepo, ApiExchange apiExchange, MarketModel marketValue)
        {
        }
    }
}