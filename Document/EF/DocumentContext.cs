using System;
using System.Collections.Generic;
using Document.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document.EF;

public partial class DocumentContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DocumentContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DocumentContext(DbContextOptions<DocumentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<ColumnTranslation> ColumnTranslations { get; set; }

    public virtual DbSet<Database> Databases { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<TableTranslation> TableTranslations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_con.ConnectionStrings["BloggingDatabase"].ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Column>(entity =>
        {
            entity.ToTable("Column");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DataType)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Table).WithMany(p => p.Columns)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Column_Table");
        });

        modelBuilder.Entity<ColumnTranslation>(entity =>
        {
            entity.HasKey(e => new { e.ColumnId, e.LanguageId });

            entity.ToTable("ColumnTranslation");

            entity.Property(e => e.LanguageId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Column).WithMany(p => p.ColumnTranslations)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ColumnTranslation_Column");

            entity.HasOne(d => d.Language).WithMany(p => p.ColumnTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ColumnTranslation_Language");
        });

        modelBuilder.Entity<Database>(entity =>
        {
            entity.ToTable("Database");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Project).WithMany(p => p.Databases)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Database_Project");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Language");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(32);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.ToTable("Table");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Database).WithMany(p => p.Tables)
                .HasForeignKey(d => d.DatabaseId)
                .HasConstraintName("FK_Table_Database");
        });

        modelBuilder.Entity<TableTranslation>(entity =>
        {
            entity.HasKey(e => new { e.TableId, e.LanguageId });

            entity.ToTable("TableTranslation");

            entity.Property(e => e.LanguageId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Language).WithMany(p => p.TableTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableTranslation_Language");

            entity.HasOne(d => d.Table).WithMany(p => p.TableTranslations)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableTranslation_Table");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
