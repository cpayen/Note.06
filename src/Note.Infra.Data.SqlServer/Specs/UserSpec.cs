using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.SqlServer.Specs
{
    public class UserSpec
    {
        public UserSpec(EntityTypeBuilder<User> entityBuilder)
        {
            EntitySpec<User>.SetEntitySpecs(entityBuilder);

            entityBuilder
                .HasIndex(o => o.Login)
                .IsUnique();

            entityBuilder
                .Property(o => o.Login)
                .IsRequired()
                .HasMaxLength(250);

            entityBuilder
                .Property(o => o.FirstName)
                .HasMaxLength(250);

            entityBuilder
                .Property(o => o.LastName)
                .HasMaxLength(250);

            entityBuilder
                .Property(o => o.Email)
                .HasMaxLength(250);

            entityBuilder
                .Ignore(o => o.FullName);
        }
    }
}
