using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;

namespace QuizzApp.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(50);

            builder.HasMany(c => c.CategoryCourses)
                   .WithOne(cc => cc.Category)
                   .HasForeignKey(q => q.CategoryId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
