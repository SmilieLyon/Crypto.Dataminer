using System;

namespace Domain.Dataminer.Entities
{
    public abstract class TradeRangeInfo
    {
        public int TradeRangeInfoId { get; set; }
        public int MarketId { get; set; }
        public int ApiId { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        //public DateTime Time { get; set; }
        public decimal? Last { get; set; }
        public decimal? Volume { get; set; }
        public Api Api { get; set; }
        public Market Market { get; set; }
    }


    public class Minute : TradeRangeInfo
    {
    }

    public class ThreeMinute : TradeRangeInfo
    {
    }

    public class FiveMinute : TradeRangeInfo
    {
    }

    public class TenMinute : TradeRangeInfo
    {
    }

    public class FifteenMinute : TradeRangeInfo
    {
    }

    public class ThirtyMinute : TradeRangeInfo
    {
    }

    public class Hour : TradeRangeInfo
    {
    }

    public class TwoHour : TradeRangeInfo
    {
    }

    public class ThreeHour : TradeRangeInfo
    {
    }

    public class FourHour : TradeRangeInfo
    {
    }

    public class SixHour : TradeRangeInfo
    {
    }

    public class TwelveHour : TradeRangeInfo
    {
    }

    public class Day : TradeRangeInfo
    {
    }

    public class ThreeDay : TradeRangeInfo
    {
    }

    public class Week : TradeRangeInfo
    {
    }

    public class TwoWeek : TradeRangeInfo
    {
    }

    public class Month : TradeRangeInfo
    {
    }
}