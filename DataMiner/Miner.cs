using AutoMapper;
using Coinigy.API.Responses;
using Serilog;

namespace DataMiner
{
    class Miner
    {
        public static void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.RollingFile("log-{Date}.log")
                    .CreateLogger();

            Mapper.Initialize(cfg => { ConfigureMaps(cfg); });
        }

        public static void ConfigureMaps(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Exchange, DataRetriever.ExchangeMarkets>();
        }

        static void Main(string[] args)
        {
            Setup();
            var retriever = new DataRetriever("37ceafda7db504c74bdf6d1bd9a82036", "2e695bf3249a7380486e6ab1d4003d0c",
                "ExchangeList.json");
            var response = retriever.GetTick("PLNX", "BTC/USDT");
        }
    }
}