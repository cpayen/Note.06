using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.SqlServer.Specs
{
    public class PageSpec
    {
        public PageSpec(EntityTypeBuilder<Page> entityBuilder)
        {
            EntitySpec<Page>.SetEntitySpecs(entityBuilder);

            entityBuilder
                .HasIndex(o => o.Name);

            entityBuilder
                .Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(250);

            entityBuilder
                .HasOne(o => o.Owner)
                .WithMany(o => o.Pages)
                .IsRequired();
        }
    }
}
