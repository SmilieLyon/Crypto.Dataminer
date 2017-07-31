using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class TradeConfiguration : EntityTypeConfiguration<Trade>
    {
        public TradeConfiguration()
            : this("dbo")
        {
        }

        public TradeConfiguration(string schema)
        {
            ToTable("Trade", schema);
            HasKey(x => new { x.ApiId, x.MarketId });

            Property(x => x.ApiId)
                .HasColumnName(@"ApiId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.MarketId)
                .HasColumnName(@"MarketId")
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.Amount)
                .HasColumnName(@"Amount")
                .IsRequired()
                .HasColumnType("decimal(10,9)");

            Property(x => x.Rate)
                .HasColumnName(@"Rate")
                .IsRequired()
                .HasColumnType("decimal(10,9)");

            Property(x => x.Cost)
                .HasColumnName(@"Cost")
                .IsRequired()
                .HasColumnType("decimal(10,9)");
        }
    }

    public enum TradeType
    {
        Buy = 1,
        Sell = 2,
        Completed = 3
    }
}
