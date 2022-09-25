using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;

namespace QuizzApp.Data.Configuration
{
    public class CategoryCourseConfiguration : IEntityTypeConfiguration<CategoryCourse>
    {
        public void Configure(EntityTypeBuilder<CategoryCourse> builder)
        {
            builder.HasKey(cc => new
            {
                cc.CategoryId,
                cc.CourseId
            });
            builder.Property(cc => cc.CourseId).IsRequired();
            builder.Property(cc => cc.CategoryId).IsRequired();
        }
    }
}
