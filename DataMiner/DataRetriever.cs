using System;
using System.Collections.Generic;
using AutoMapper;
using Coinigy.API;
using Coinigy.API.Responses;
using Newtonsoft.Json;

namespace DataMiner
{
    public class ExchangeMarketDataRetriever
    {
        public ExchangeMarketDataRetriever(string apikey, string apisecret, StorageAppender appender)
        {
            Appender = appender;
            Api = new CoinigyApi(apikey, apisecret, "https://api.coinigy.com/api/v1");
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

            var exchanges = Api.Exchanges();
            Markets = new FileStore<IList<ExchangeMarkets>>
            {
                Expires = TimeSpan.FromDays(7),
                Data = Mapper.Map<IList<ExchangeMarkets>>(exchanges.data)
            };

            foreach (var exchange in Markets.Data)
            {
                exchange.Markets = Mapper.Map<IList<MarketValue>>(Api.Markets(exchange.exch_code).data);
            }
            var output = JsonConvert.SerializeObject(Markets, Formatting.Indented);
            Appender.Save(output);
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