using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;
using System;

namespace QuizzApp.Data.Configuration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<TestQuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<TestQuestionAnswer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.TestQuestion)
                   .WithMany(t => t.Answers)
                   .HasForeignKey(t => t.TestId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
