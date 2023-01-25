using Document.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Data.EF.Maps
{
    public class DatabaseMapping : IEntityTypeConfiguration<Database>
    {
        public void Configure(EntityTypeBuilder<Database> builder)
        {
            builder.ToTable("Database");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Name).HasMaxLength(255);

            builder.HasOne(d => d.Project).WithMany(p => p.Databases)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Database_Project");
        }
    }
}
