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
            HasKey(x => new { x.TradeId });

            Property(x => x.TradeId)
                .HasColumnName(@"TradeId")
                .IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

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
            
            HasRequired(asset => asset.Market)
                .WithMany(i => i.Trades)
                .HasForeignKey(asset => asset.MarketId);

            HasRequired(asset => asset.Api)
                .WithMany(i => i.Trades)
                .HasForeignKey(asset => asset.ApiId);

            Map<Bid>(m => m.Requires("Type").HasValue(TradeType.Bid))
                .Map<Ask>(m => m.Requires("Type").HasValue(TradeType.Ask))
                .Map<Sale>(m => m.Requires("Type").HasValue(TradeType.Sale));
        }
    }
}
