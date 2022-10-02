using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;
using System;

namespace QuizzApp.Data.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CourseName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CreatedTime).HasDefaultValue(DateTime.Now);

            builder.HasMany(c => c.CategoryCourses)
                   .WithOne(cc => cc.Course)
                   .HasForeignKey(q => q.CourseId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Tests)
                .WithOne(t => t.Course)
                .HasForeignKey(t => t.CourseId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}