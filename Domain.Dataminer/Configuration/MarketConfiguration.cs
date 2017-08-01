using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class MarketConfiguration : EntityTypeConfiguration<Market>
    {
        public MarketConfiguration()
            : this("dbo")
        {
        }

        public MarketConfiguration(string schema)
        {
            ToTable("Market", schema);
            HasKey(x => x.MarketId);

            Property(x => x.MarketId)
                .HasColumnName(@"MarketId")
                .IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.ExchangeId)
                .HasColumnName(@"ExchangeId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.PrimaryAssetId)
                .HasColumnName(@"PrimaryAssetId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.SecondaryAssetId)
                .HasColumnName(@"SecondaryAssetId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.Name)
                .HasColumnName(@"Name")
                .IsRequired()
                .HasColumnType("varchar");

            HasRequired(asset => asset.Exchange)
                .WithMany(i => i.Markets)
                .HasForeignKey(asset => asset.ExchangeId);

            HasRequired(asset => asset.PrimaryAsset)
                .WithMany(i => i.PrimaryMarkets)
                .HasForeignKey(asset => asset.PrimaryAssetId);

            HasRequired(asset => asset.SecondaryAsset)
                .WithMany(i => i.SecondaryMarkets)
                .HasForeignKey(asset => asset.SecondaryAssetId);
        }
    }
}