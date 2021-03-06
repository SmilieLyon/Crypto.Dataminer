﻿using System.Collections.Generic;

namespace Domain.Dataminer.Entities
{
    public class Asset
    {
        public Asset()
        {
            ApiAssets = new HashSet<ApiAsset>();
            PrimaryMarkets = new HashSet<Market>();
            SecondaryMarkets = new HashSet<Market>();
        }

        public int AssetId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public ICollection<ApiAsset> ApiAssets { get; set; }
        public ICollection<Market> PrimaryMarkets { get; set; }
        public ICollection<Market> SecondaryMarkets { get; set; }
    }
}