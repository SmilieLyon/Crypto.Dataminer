namespace Domain.Dataminer.Entities
{
    public class ApiMarket
    {
        public int MarketId { get; set; }
        public int ApiId { get; set; }
        public string ExternalId { get; set; }
        public string Code { get; set; }
        public Api Api { get; set; }
        public Market Market { get; set; }
    }
}