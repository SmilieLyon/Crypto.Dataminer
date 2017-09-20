using AutoMapper;
using Coinigy.API.Responses;

namespace DataMiner.CoinigyDataAdapter
{
    public class CoinigyMaps
    {
        public static void ConfigureMaps(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Exchange, ExchangeMarketDataRetriever.ExchangeMarkets>();
            cfg.CreateMap<Market, ExchangeMarketDataRetriever.MarketValue>();
        }
    }
}