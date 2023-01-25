using Document.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Data.EF.Maps
{
    public class ColumnTranslationMapping : IEntityTypeConfiguration<ColumnTranslation>
    {
        public void Configure(EntityTypeBuilder<ColumnTranslation> builder)
        {
            builder.HasKey(e => new { e.ColumnId, e.LanguageId });

            builder.ToTable("ColumnTranslation");

            builder.Property(e => e.LanguageId)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.HasOne(d => d.Column).WithMany(p => p.ColumnTranslations)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ColumnTranslation_Column");

            builder.HasOne(d => d.Language).WithMany(p => p.ColumnTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ColumnTranslation_Language");
        }
    }
}
