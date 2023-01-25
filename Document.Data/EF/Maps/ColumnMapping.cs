using Document.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Data.EF.Maps
{
    public class ColumnMapping : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.ToTable("Column");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.DataType)
                .HasMaxLength(64)
                .IsUnicode(false);
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasOne(d => d.Table).WithMany(p => p.Columns)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
