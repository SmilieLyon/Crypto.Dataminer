using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class ApiAssetConfiguration : EntityTypeConfiguration<ApiAsset>
    {
        public ApiAssetConfiguration()
            : this("dbo")
        {
        }

        public ApiAssetConfiguration(string schema)
        {
            ToTable("ApiAsset", schema);
            HasKey(x => new {x.ApiId, x.AssetId});

            Property(x => x.ApiId)
                .HasColumnName(@"ApiId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.AssetId)
                .HasColumnName(@"AssetId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.Code)
                .HasColumnName(@"Code")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            HasRequired(asset => asset.Api)
                .WithMany(i => i.ApiAssets)
                .HasForeignKey(asset => asset.ApiId);

            HasRequired(asset => asset.Asset)
                .WithMany(i => i.ApiAssets)
                .HasForeignKey(asset => asset.AssetId);
        }
    }
}