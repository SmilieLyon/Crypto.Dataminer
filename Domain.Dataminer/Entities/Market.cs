using System.Collections.Generic;

namespace Domain.Dataminer.Entities
{
    public class Market
    {
        public Market()
        {
            ApiMarkets = new HashSet<ApiMarket>();
            Trades = new HashSet<Trade>();
            TradeRangeInfos = new HashSet<TradeRangeInfo>();
        }

        public int MarketId { get; set; }
        public int ExchangeId { get; set; }
        public int PrimaryAssetId { get; set; }
        public int SecondaryAssetId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public Asset PrimaryAsset { get; set; }
        public Asset SecondaryAsset { get; set; }
        public Exchange Exchange { get; set; }
        public ICollection<ApiMarket> ApiMarkets { get; set; }
        public ICollection<Trade> Trades { get; set; }
        public ICollection<TradeRangeInfo> TradeRangeInfos { get; set; }
    }
}