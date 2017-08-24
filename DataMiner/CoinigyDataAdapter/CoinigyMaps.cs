using AutoMapper;

namespace DataMiner.CoinigyDataAdapter
{
    public class CoinigyMaps
    {
        public static void ConfigureMaps(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Coinigy.API.Responses.Exchange, ExchangeMarketDataRetriever.ExchangeMarkets>();
            cfg.CreateMap<Coinigy.API.Responses.Market, ExchangeMarketDataRetriever.MarketValue>();
        }
    }
}