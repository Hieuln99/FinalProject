using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;
using System;

namespace QuizzApp.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(cm => cm.Id);
            builder.Property(cm => cm.Email).IsRequired();
            builder.Property(cm => cm.CommentHeader).IsRequired();
            builder.Property(cm => cm.CommentText).IsRequired().HasMaxLength(100);
            builder.Property(cm => cm.CommentTime).HasDefaultValue(DateTime.Now);

            builder.HasOne(cm => cm.Post)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(p => p.PostId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
