using Document.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Data.EF.Maps
{
    public class TableTranslationMapping : IEntityTypeConfiguration<TableTranslation>
    {
        public void Configure(EntityTypeBuilder<TableTranslation> builder)
        {
            builder.HasKey(e => new { e.TableId, e.LanguageId });

            builder.ToTable("TableTranslation");

            builder.Property(e => e.LanguageId)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.HasOne(d => d.Language).WithMany(p => p.TableTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableTranslation_Language");

            builder.HasOne(d => d.Table).WithMany(p => p.TableTranslations)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableTranslation_Table");
        }
    }
}
