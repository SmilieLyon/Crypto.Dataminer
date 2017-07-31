using System;

namespace Domain.Dataminer.Entities
{
    public abstract class Market
    {
        public int MarketId { get; set; }
        public int ExchangeId { get; set; }
        public int PrimaryAssetId { get; set; }
        public int SecondaryAssetId { get; set; }
        public string Name { get; set; }
        public Asset PrimaryAsset { get; set; }
        public Asset SecondaryAsset { get; set; }
        public Exchange Exchange { get; set; }
    }
}