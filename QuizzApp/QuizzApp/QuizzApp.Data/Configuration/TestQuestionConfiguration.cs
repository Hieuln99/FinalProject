using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;

namespace QuizzApp.Data.Configuration
{
    public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.HasKey(tq => tq.TestId);

            builder.HasOne(tq => tq.Test)
                   .WithMany(te => te.TestQuestions)
                   .HasForeignKey(tq => tq.TestExamId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tq => tq.Question)
                   .WithMany(te => te.TestQuestions)
                   .HasForeignKey(tq => tq.QuestionId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}
