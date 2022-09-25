using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;

namespace QuizzApp.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.Categories)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Posts)
               .WithOne(c => c.User)
               .HasForeignKey(p => p.UserId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
