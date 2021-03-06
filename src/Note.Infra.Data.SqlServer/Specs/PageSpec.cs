﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.SqlServer.Specs
{
    public class PageSpec
    {
        public PageSpec(EntityTypeBuilder<Page> entityBuilder)
        {
            EntitySpec<Page>.SetEntitySpecs(entityBuilder);

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
                .WithMany(o => o.Pages)
                .IsRequired();
        }
    }
}
