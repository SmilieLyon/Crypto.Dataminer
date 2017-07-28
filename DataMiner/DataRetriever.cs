using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Coinigy.API;
using Coinigy.API.Responses;
using Newtonsoft.Json;
using Serilog;

namespace DataMiner
{
    public class DataRetriever
    {
        private CoinigyApi Api { get; }
        private string ExchangeListFileName { get; }

        private FileStore<IList<ExchangeMarkets>> Markets { get; set; }

        private void ListMarkets(string filename)
        {
            if (Markets == null && File.Exists(filename))
            {
                Markets = JsonConvert.DeserializeObject<FileStore<IList<ExchangeMarkets>>>(File.ReadAllText(filename));
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
                exchange.Markets = Api.Markets(exchange.exch_code).data;
            }
            var output = JsonConvert.SerializeObject(Markets);
            File.WriteAllText(filename, output);
        }

        public DataRetriever(string apikey, string apisecret, string exchangeFile)
        {
            ExchangeListFileName = exchangeFile;
            Api = new CoinigyApi(apikey, apisecret);
        }

        public ticker_response GetTick(string exchange, string code)
        {
            //var data = Api.Data("PLNX", "BTC/USDT", MarketDataType.history);
            ListMarkets(ExchangeListFileName);
            return Api.Ticker(exchange, code);
        }

        public class ExchangeMarkets : Exchange
        {
            public List<Market> Markets { get; set; }
        }
    }
}