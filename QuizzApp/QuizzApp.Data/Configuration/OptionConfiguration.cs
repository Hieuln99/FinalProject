using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizzApp.Data.Entities;

namespace QuizzApp.Data.Configuration
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OptionName).IsRequired();
        }
    }
}
