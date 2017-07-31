using System.Data.Entity.ModelConfiguration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer.Configuration
{
    public class ApiConfiguration : EntityTypeConfiguration<Api>
    {
        public ApiConfiguration()
            : this("dbo")
        {
        }

        public ApiConfiguration(string schema)
        {
            ToTable("Api", schema);
            HasKey(x => x.ApiId);

            Property(x => x.ApiId)
                .HasColumnName(@"ApiId")
                .IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .HasColumnName(@"Name")
                .IsRequired()
                .HasColumnType("varchar");
        }
    }
}