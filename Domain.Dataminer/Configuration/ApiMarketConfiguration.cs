using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class ApiMarketConfiguration : EntityTypeConfiguration<ApiMarket>
    {
        public ApiMarketConfiguration()
            : this("dbo")
        {
        }

        public ApiMarketConfiguration(string schema)
        {
            ToTable("ApiMarket", schema);
            HasKey(x => new {x.ApiId, x.MarketId});

            Property(x => x.MarketId)
                .HasColumnName(@"MarketId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.ApiId)
                .HasColumnName(@"ApiId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.ExternalId)
                .HasColumnName(@"ExternalName")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            Property(x => x.Code)
                .HasColumnName(@"Code")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            HasRequired(asset => asset.Api)
                .WithMany(i => i.ApiMarkets)
                .HasForeignKey(asset => asset.ApiId);

            HasRequired(asset => asset.Market)
                .WithMany(i => i.ApiMarkets)
                .HasForeignKey(asset => asset.MarketId);
        }
    }
}