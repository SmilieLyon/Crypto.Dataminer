using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class AssetConfiguration : EntityTypeConfiguration<Asset>
    {
        public AssetConfiguration()
            : this("dbo")
        {
        }

        public AssetConfiguration(string schema)
        {
            ToTable("Asset", schema);
            HasKey(x => new {x.AssetId});

            Property(x => x.AssetId)
                .HasColumnName(@"AssetId")
                .IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .HasColumnName(@"Name")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            Property(x => x.Url)
                .HasColumnName(@"Url")
                .HasColumnType("varchar")
                .HasMaxLength(200);

            Property(x => x.Description)
                .HasColumnName(@"Description")
                .HasColumnType("varchar")
                .HasMaxLength(200);
        }
    }
}