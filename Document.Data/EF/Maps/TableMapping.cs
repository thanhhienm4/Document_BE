using Document.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Data.EF.Maps
{
    public class TableMapping : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Table");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasOne(d => d.Database).WithMany(p => p.Tables)
                .HasForeignKey(d => d.DatabaseId)
                .HasConstraintName("FK_Table_Database");
        }
    }
}
