using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities.Base;
using System;

namespace Note.Infra.Data.SqlServer.Specs
{
    internal class EntitySpec<T> where T : Entity
    {
        public static void SetEntitySpecs(EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder
                .HasKey(o => o.Id);

            entityBuilder
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            entityBuilder
                .Property(o => o.CreatedAt)
                .IsRequired();
        }
    }
}
