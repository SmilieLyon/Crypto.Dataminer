using System;
using System.IO;
using AutoMapper;
using Domain.Dataminer;
using Domain.Dataminer.Entities;
using Newtonsoft.Json;
using Serilog;
using DataMiner.CoinigyDataAdapter;

namespace DataMiner
{
    public abstract class StorageAppender
    {
        public abstract bool HasData { get; }
        public abstract void Save(object data);
        public abstract string ReadAllText();
    }

    public class JsonFileAppender : StorageAppender
    {
        public JsonFileAppender(string filename, TradeRangeInfoPeriod period)
        {
            Filename = $"{filename}{GetPeriodExtension(period)}.json";
        }

        public string Filename { get; set; }
        public override bool HasData => File.Exists(Filename);

        public static string GetPeriodExtension(TradeRangeInfoPeriod period)
        {
            const string delimiter = "_";
            switch (period)
            {
                case TradeRangeInfoPeriod.Minute:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHHmm");
                case TradeRangeInfoPeriod.FiveMinute:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH") + (DateTime.Now.Minute / 5).ToString("D2");
                case TradeRangeInfoPeriod.FifteenMinute:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH") + DateTime.Now.Minute / 15;
                case TradeRangeInfoPeriod.ThirtyMinute:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH") + DateTime.Now.Minute / 30;
                case TradeRangeInfoPeriod.Hour:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH");
                case TradeRangeInfoPeriod.FourHour:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH") + DateTime.Now.Hour / 4;
                case TradeRangeInfoPeriod.Day:
                    return delimiter + DateTime.Now.ToString("yyyyMMdd");
                case TradeRangeInfoPeriod.ThreeDay:
                    return delimiter + DateTime.Now.ToString("yyyyMM") + (DateTime.Now.DayOfYear / 3).ToString("D3");
                case TradeRangeInfoPeriod.Week:
                    return delimiter + DateTime.Now.ToString("yyyyMM") + (DateTime.Now.DayOfYear / 7).ToString("D2");
                case TradeRangeInfoPeriod.TwoWeek:
                    return delimiter + DateTime.Now.ToString("yyyyMM") + (DateTime.Now.DayOfYear / 14).ToString("D2");
                case TradeRangeInfoPeriod.Month:
                    return delimiter + DateTime.Now.ToString("yyyyMM");
                default:
                    return string.Empty;
            }
        }

        public override void Save(object data)
        {
            File.WriteAllText(Filename, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        public override string ReadAllText()
        {
            return File.ReadAllText(Filename);
        }
    }

    internal class Miner
    {
        public static void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile("log-{Date}.log")
                .CreateLogger();

            Mapper.Initialize(cfg => { CoinigyMaps.ConfigureMaps(cfg); });
        }
        
        private static void Main(string[] args)
        {
            Setup();
            var retriever = new ExchangeMarketDataRetriever("37ceafda7db504c74bdf6d1bd9a82036",
                "2e695bf3249a7380486e6ab1d4003d0c");
            //retriever.StartMiner("PLNX", "BTC/USDT", TradeRangeInfoPeriod.Hour);
            //retriever.StartMiner("PLNX", "BTC/USDT", TradeRangeInfoPeriod.TenMinute);
            //retriever.StartMiner("PLNX", "BTC/USDT", TradeRangeInfoPeriod.FourHour);
            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
    }
}