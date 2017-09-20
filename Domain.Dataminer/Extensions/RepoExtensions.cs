using System.Linq;
using Domain.Base;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Extensions
{
    public static class RepoExtensions
    {
        public static Api GetCreateApi(this IGenericRepository<Api> apiRepo, string apiName)
        {
            var api = apiRepo.Query().FirstOrDefault(x => x.Name == apiName);
            if (api == null)
            {
                api = new Api
                {
                    Name = apiName
                };
                apiRepo.Insert(api);
                apiRepo.SaveChanges();
            }
            return api;
        }

        public static Exchange CheckExchangeExistsInDb(this IGenericRepository<Exchange> exchangeRepo,
            string exchangeName, string exchangeUrl)
        {
            var exchange = exchangeRepo.Query().FirstOrDefault(x => x.Name == exchangeName);
            if (exchange == null)
            {
                exchange = new Exchange
                {
                    Name = exchangeName,
                    Url = exchangeUrl
                };
                exchangeRepo.Insert(exchange);
                exchangeRepo.SaveChanges();
            }
            return exchange;
        }

        public static ApiExchange CheckExchangeApiExistsInDb(
            this IGenericRepository<ApiExchange> apiExchangeRepo,
            Api coinigyApi,
            Exchange exchange,
            bool balanceEnabled,
            bool tradeEnabled,
            string code,
            string externalId,
            decimal fee
        )
        {
            var api = apiExchangeRepo.Query()
                .FirstOrDefault(x => x.ExchangeId == exchange.ExchangeId && x.ApiId == coinigyApi.ApiId);
            if (api == null)
            {
                api = new ApiExchange
                {
                    Api = coinigyApi,
                    Exchange = exchange,
                    BalanceEnabled = balanceEnabled,
                    TradeEnabled = tradeEnabled,
                    Code = code,
                    ExternalId = externalId,
                    Fee = fee
                };
                apiExchangeRepo.Insert(api);
                apiExchangeRepo.SaveChanges();
            }
            return api;
        }

        public static Asset CheckAssetExistsInDb(this IGenericRepository<Asset> assetRepo, string assetName)
        {
            var market = assetRepo.Query().FirstOrDefault(x => x.Name == assetName);
            if (market == null)
            {
                market = new Asset
                {
                    Name = assetName,
                    Description = assetName
                };
                assetRepo.Insert(market);
                assetRepo.SaveChanges();
            }
            return market;
        }
    }
}