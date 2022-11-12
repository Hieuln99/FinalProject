using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;

namespace QuizzApp.Data.Configuration
{
    public class ListApprovesConfiguration : IEntityTypeConfiguration<ListApproves>
    {
        public void Configure(EntityTypeBuilder<ListApproves> builder)
        {
            builder.HasKey(u => new
            {
                u.CourseId,
                u.UserId
            });
            builder.ToTable("ListApproves", "dbo");
            builder.Property(cc => cc.CourseId).IsRequired();
            builder.Property(cc => cc.UserId).IsRequired();
        }
    }
}
