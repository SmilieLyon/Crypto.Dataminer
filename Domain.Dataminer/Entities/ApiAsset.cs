namespace Domain.Dataminer.Entities
{
    public class ApiAsset
    {
        public int AssetId { get; set; }
        public int ApiId { get; set; }
        public string Code { get; set; }

        public Api Api { get; set; }
        public Asset Asset { get; set; }
    }
}