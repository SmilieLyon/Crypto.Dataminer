namespace Domain.Dataminer.Entities
{
    public abstract class Trade
    {
        public int TradeId { get; set; }
        public int MarketId { get; set; }
        public int ApiId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Cost { get; set; }
        public Api Api { get; set; }
        public Market Market { get; set; }
    }

    public class Bid : Trade
    {
    }

    public class Ask : Trade
    {
    }

    public class Sale : Trade
    {
    }
}