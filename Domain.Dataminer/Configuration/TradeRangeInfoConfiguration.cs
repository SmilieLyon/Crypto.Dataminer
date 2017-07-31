using System;
using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class TradeRangeInfoConfiguration : EntityTypeConfiguration<TradeRangeInfo>
    {
        public TradeRangeInfoConfiguration()
            : this("dbo")
        {
        }

        public TradeRangeInfoConfiguration(string schema)
        {
            ToTable("TradeRangeInfo", schema);
            HasKey(x => new { x.ApiId, x.MarketId });

            Property(x => x.MarketId)
                .HasColumnName(@"MarketId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.ApiId)
                .HasColumnName(@"ApiId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.High)
                .HasColumnName(@"High")
                .IsRequired()
                .HasColumnType("decimal(10,9)");

            Property(x => x.Low)
                .HasColumnName(@"Low")
                .IsRequired()
                .HasColumnType("decimal(10,9)");

            Property(x => x.Open)
                .HasColumnName(@"Open")
                .IsRequired()
                .HasColumnType("decimal(10,9)");

            Property(x => x.Close)
                .HasColumnName(@"Close")
                .IsRequired()
                .HasColumnType("decimal(10,9)");

            Property(x => x.Last)
                .HasColumnName(@"Last")
                .IsRequired()
                .HasColumnType("decimal(10,9)");

            Property(x => x.Volume)
                .HasColumnName(@"Volume")
                .IsRequired()
                .HasColumnType("decimal(10,9)");
        }

        public int ExchangeMarketId { get; set; }
        public int ApiId { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public DateTime Time { get; set; }
        public decimal Last { get; set; }
        public decimal Volume { get; set; }
    }
}