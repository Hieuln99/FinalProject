using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;

namespace QuizzApp.Data.Configuration
{
    internal class UserCoursePaymentConfig : IEntityTypeConfiguration<UserCoursePayment>
    {
        public void Configure(EntityTypeBuilder<UserCoursePayment> builder)
        {
            builder.HasKey(u => new
            {
                u.CourseId,
                u.UserId
            });
            builder.ToTable("UserCoursePayment", "dbo");
            builder.Property(cc => cc.CourseId).IsRequired();
            builder.Property(cc => cc.UserId).IsRequired();
        }
    }
}
