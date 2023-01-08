using Document.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.EF.Maps
{
    public class LanguageMapping : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Language");

            builder.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            builder.Property(e => e.Name).HasMaxLength(32);
        }
    }
}
