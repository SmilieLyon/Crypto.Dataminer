﻿using System;
using AutoMapper;
using Serilog;
using System.IO;
using Domain.Dataminer.Entities;
using Newtonsoft.Json;
using Exchange = Coinigy.API.Responses.Exchange;
using Market = Coinigy.API.Responses.Market;

namespace DataMiner
{
    public abstract class StorageAppender
    {
        public abstract void Save(object data);
        public abstract string ReadAllText();
        public abstract bool HasData { get; }
    }

    public class JsonFileAppender : StorageAppender
    {
        public string Filename { get; set; }
        public override bool HasData { get => File.Exists(Filename); }

        public JsonFileAppender(string filename, TradeRangeInfoPeriod period)
        {
            Filename = $"{filename}{GetPeriodExtension(period)}.json";
        }

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
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH") + (DateTime.Now.Minute / 15);
                case TradeRangeInfoPeriod.ThirtyMinute:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH") + (DateTime.Now.Minute / 30);
                case TradeRangeInfoPeriod.Hour:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH");
                case TradeRangeInfoPeriod.FourHour:
                    return delimiter + DateTime.Now.ToString("yyyyMMddHH") + (DateTime.Now.Hour / 4);
                case TradeRangeInfoPeriod.Day:
                    return delimiter + DateTime.Now.ToString("yyyyMMdd");
                case TradeRangeInfoPeriod.TwoDay:
                    return delimiter + DateTime.Now.ToString("yyyyMM") + (DateTime.Now.DayOfYear / 2).ToString("D3");
                case TradeRangeInfoPeriod.Week:
                    return delimiter + DateTime.Now.ToString("yyyyMM")+(DateTime.Now.DayOfYear/7).ToString("D2");
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
            cfg.CreateMap<Exchange, ExchangeMarketDataRetriever.ExchangeMarkets>();
            cfg.CreateMap<Market, ExchangeMarketDataRetriever.MarketValue>();
        }

        static void Main(string[] args)
        {
            Setup();
            var retriever = new ExchangeMarketDataRetriever("37ceafda7db504c74bdf6d1bd9a82036", "2e695bf3249a7380486e6ab1d4003d0c",
                new JsonFileAppender("ExchangeList", TradeRangeInfoPeriod.Week));
            var response = retriever.GetTick("PLNX", "BTC/USDT");
        }
    }
}