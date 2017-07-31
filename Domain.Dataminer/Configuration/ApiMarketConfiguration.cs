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
            HasKey(x => new { x.ApiId, x.MarketId });

            Property(x => x.MarketId)
                .HasColumnName(@"MarketId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.ApiId)
                .HasColumnName(@"ApiId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.ExternalName)
                .HasColumnName(@"ExternalName")
                .IsRequired()
                .HasColumnType("varchar");

            Property(x => x.Code)
                .HasColumnName(@"Code")
                .IsRequired()
                .HasColumnType("varchar");
        }
    }
}