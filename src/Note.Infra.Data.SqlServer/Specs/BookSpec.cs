using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.SqlServer.Specs
{
    public class BookSpec
    {
        public BookSpec(EntityTypeBuilder<Book> entityBuilder)
        {
            EntitySpec<Book>.SetEntitySpecs(entityBuilder);

            entityBuilder
                .HasIndex(o => o.Title);

            entityBuilder
                .Property(o => o.Title)
                .IsRequired()
                .HasMaxLength(250);

            entityBuilder
                .HasIndex(o => o.Slug);

            entityBuilder
                .Property(o => o.Slug)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder
                .HasOne(o => o.Owner)
                .WithMany(o => o.Books)
                .IsRequired();

            entityBuilder
                .Ignore(o => o.PageCount);
        }
    }
}
