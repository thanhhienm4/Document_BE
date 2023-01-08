using Document.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.EF.Maps
{
    public class ColumnMaping : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.ToTable(nameof(Column));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
        }
    }
}
