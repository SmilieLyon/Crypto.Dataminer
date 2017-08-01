using System.ComponentModel.DataAnnotations.Schema;
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
            HasKey(x => new {x.TradeRangeInfoId});

            Property(x => x.TradeRangeInfoId)
                .HasColumnName(@"TradeRangeInfoId")
                .IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

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
                .HasColumnType("decimal")
                .HasPrecision(10, 9);

            Property(x => x.Low)
                .HasColumnName(@"Low")
                .HasColumnType("decimal")
                .HasPrecision(10, 9);

            Property(x => x.Open)
                .HasColumnName(@"OpenRange")
                .HasColumnType("decimal")
                .HasPrecision(10, 9);

            Property(x => x.Close)
                .HasColumnName(@"CloseRange")
                .HasColumnType("decimal")
                .HasPrecision(10, 9);

            Property(x => x.Last)
                .HasColumnName(@"Last")
                .HasColumnType("decimal")
                .HasPrecision(10, 9);

            Property(x => x.Volume)
                .HasColumnName(@"Volume")
                .HasColumnType("decimal")
                .HasPrecision(10, 9);

            HasRequired(asset => asset.Market)
                .WithMany(i => i.TradeRangeInfos)
                .HasForeignKey(asset => asset.MarketId);

            HasRequired(asset => asset.Api)
                .WithMany(i => i.TradeRangeInfos)
                .HasForeignKey(asset => asset.ApiId);

            Map<Minute>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.Minute))
                .Map<ThreeMinute>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.ThreeMinute))
                .Map<FiveMinute>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.FiveMinute))
                .Map<TenMinute>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.TenMinute))
                .Map<FifteenMinute>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.FifteenMinute))
                .Map<ThirtyMinute>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.ThirtyMinute))
                .Map<Hour>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.Hour))
                .Map<TwoHour>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.TwoHour))
                .Map<ThreeHour>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.ThreeHour))
                .Map<FourHour>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.FourHour))
                .Map<SixHour>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.SixHour))
                .Map<TwelveHour>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.TwelveHour))
                .Map<Day>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.Day))
                .Map<ThreeDay>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.ThreeDay))
                .Map<Week>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.Week))
                .Map<TwoWeek>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.TwoWeek))
                .Map<Month>(m => m.Requires("Type").HasValue((int) TradeRangeInfoPeriod.Month));
        }
    }
}