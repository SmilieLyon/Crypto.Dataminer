using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class ExchangeConfiguration : EntityTypeConfiguration<Exchange>
    {
        public ExchangeConfiguration()
            : this("dbo")
        {
        }

        public ExchangeConfiguration(string schema)
        {
            ToTable("Exchange", schema);
            HasKey(x => x.ExchangeId);

            Property(x => x.ExchangeId)
                .HasColumnName(@"ExchangeId")
                .IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .HasColumnName(@"Name")
                .IsRequired()
                .HasColumnType("varchar");

            Property(x => x.Url)
                .HasColumnName(@"Url")
                .HasColumnType("varchar");
        }
    }
}