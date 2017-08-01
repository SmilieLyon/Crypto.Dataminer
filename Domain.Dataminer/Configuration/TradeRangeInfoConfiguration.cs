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
            HasKey(x => new { x.TradeRangeInfoId });

            Property(x => x.TradeRangeInfoId)
                .HasColumnName(@"TradeRangeInfoId")
                .IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

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

            HasRequired(asset => asset.Market)
                .WithMany(i => i.TradeRangeInfos)
                .HasForeignKey(asset => asset.MarketId);

            HasRequired(asset => asset.Api)
                .WithMany(i => i.TradeRangeInfos)
                .HasForeignKey(asset => asset.ApiId);

            Map<Minute>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.Minute))
                .Map<ThreeMinute>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.ThreeMinute))
                .Map<FiveMinute>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.FiveMinute))
                .Map<TenMinute>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.TenMinute))
                .Map<FifteenMinute>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.FifteenMinute))
                .Map<ThirtyMinute>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.ThirtyMinute))
                .Map<Hour>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.Hour))
                .Map<TwoHour>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.TwoHour))
                .Map<ThreeHour>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.ThreeHour))
                .Map<FourHour>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.FourHour))
                .Map<SixHour>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.SixHour))
                .Map<TwelveHour>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.TwelveHour))
                .Map<Day>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.Day))
                .Map<ThreeDay>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.ThreeDay))
                .Map<Week>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.Week))
                .Map<TwoWeek>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.TwoWeek))
                .Map<Month>(m => m.Requires("Type").HasValue(TradeRangeInfoPeriod.Month));
        }
    }
}