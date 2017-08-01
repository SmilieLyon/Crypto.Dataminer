namespace Domain.Dataminer.Entities
{
    public class ApiExchange
    {
        public int ExchangeId { get; set; }
        public int ApiId { get; set; }
        public string ExternalId { get; set; }
        public string Code { get; set; }
        public decimal? Fee { get; set; }
        public bool? BalanceEnabled { get; set; }
        public bool? TradeEnabled { get; set; }
        public Api Api { get; set; }
        public Exchange Exchange { get; set; }
    }
}