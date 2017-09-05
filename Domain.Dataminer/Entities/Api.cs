using System.Collections.Generic;
using System;

namespace Domain.Dataminer.Entities
{
    public class Api
    {
        public Api()
        {
            ApiAssets = new HashSet<ApiAsset>();
            ApiExchanges = new HashSet<ApiExchange>();
            ApiMarkets = new HashSet<ApiMarket>();
            Trades = new HashSet<Trade>();
            TradeRangeInfos = new HashSet<TradeRangeInfo>();
        }

        public int ApiId { get; set; }
        public string Name { get; set; }

        public ICollection<ApiAsset> ApiAssets { get; set; }
        public ICollection<ApiExchange> ApiExchanges { get; set; }
        public ICollection<ApiMarket> ApiMarkets { get; set; }
        public ICollection<Trade> Trades { get; set; }
        public ICollection<TradeRangeInfo> TradeRangeInfos { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}