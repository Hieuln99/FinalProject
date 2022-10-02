using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;

namespace QuizzApp.Data.Configuration
{
    internal class TestExamConfiguration : IEntityTypeConfiguration<TestExam>
    {
        public void Configure(EntityTypeBuilder<TestExam> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(te => te.Course)
               .WithMany(c => c.Tests)
               .HasForeignKey(te => te.CourseId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
