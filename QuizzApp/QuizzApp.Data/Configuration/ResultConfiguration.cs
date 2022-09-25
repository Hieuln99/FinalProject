//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using QuizzApp.Data.Entities;

//namespace QuizzApp.Data.Configuration
//{
//    public class ResultConfiguration : IEntityTypeConfiguration<Result>
//    {
//        public void Configure(EntityTypeBuilder<Result> builder)
//        {
//            builder.HasKey(r => r.Id);
//            builder.Property(r => r.AnswerText).IsRequired().HasMaxLength(100);
//        }
//    }
//}
