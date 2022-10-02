using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;
using System;

namespace QuizzApp.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p .Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p .Description).IsRequired(true).HasMaxLength(100);
            builder.Property(p => p .Content).IsRequired(false).HasMaxLength(250);
            builder.Property(p => p .PostedOn).HasDefaultValue(DateTime.Now);
        }
    }
}
