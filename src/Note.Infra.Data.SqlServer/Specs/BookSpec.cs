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
                .HasIndex(o => o.Name);

            entityBuilder
                .Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(250);

            entityBuilder
                .HasOne(o => o.Owner)
                .WithMany(o => o.Books)
                .IsRequired();

            entityBuilder
                .Ignore(o => o.PageCount);
        }
    }
}
