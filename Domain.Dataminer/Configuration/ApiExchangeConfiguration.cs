using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class ApiExchangeConfiguration : EntityTypeConfiguration<ApiExchange>
    {
        public ApiExchangeConfiguration()
            : this("dbo")
        {
        }

        public ApiExchangeConfiguration(string schema)
        {
            ToTable("ApiExchange", schema);
            HasKey(x => new {x.ApiId, x.ExchangeId});

            Property(x => x.ApiId)
                .HasColumnName(@"ApiId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.ExchangeId)
                .HasColumnName(@"ExchangeId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.Code)
                .HasColumnName(@"Code")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            Property(x => x.ExternalId)
                .HasColumnName(@"ExternalId")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            Property(x => x.Fee)
                .HasColumnName(@"Fee")
                .HasColumnType("decimal")
                .HasPrecision(10, 9);

            Property(x => x.BalanceEnabled)
                .HasColumnName(@"BalanceEnabled")
                .HasColumnType("Bit");

            Property(x => x.TradeEnabled)
                .HasColumnName(@"TradeEnabled")
                .HasColumnType("Bit");

            HasRequired(asset => asset.Api)
                .WithMany(i => i.ApiExchanges)
                .HasForeignKey(asset => asset.ApiId);

            HasRequired(asset => asset.Exchange)
                .WithMany(i => i.ApiExchanges)
                .HasForeignKey(asset => asset.ExchangeId);
        }
    }
}