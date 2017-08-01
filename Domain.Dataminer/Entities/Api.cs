using System.Collections;
using System.Collections.Generic;

namespace Domain.Dataminer.Entities
{
    public class Api
    {
        public int ApiId { get; set; }
        public string Name { get; set; }

        public ICollection<ApiAsset> ApiAssets { get; set; }
        public ICollection<ApiExchange> ApiExchanges { get; set; }
        public ICollection<ApiMarket> ApiMarkets { get; set; }
        public ICollection<Trade> Trades { get; set; }
        public ICollection<TradeRangeInfo> TradeRangeInfos { get; set; }
    }
}