using Document.EF.Entities;
using Document.EF.Maps;
using Microsoft.EntityFrameworkCore;

namespace Document.EF;

// ReSharper disable once PartialTypeWithSinglePart
public partial class DocumentContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DocumentContext(DbContextOptions<DocumentContext> options)
        : base(options)
    {
        _configuration = new ConfigurationBuilder().Build();
    }

    public virtual DbSet<Column> Columns { get; set; } = null!;

    public virtual DbSet<ColumnTranslation> ColumnTranslations { get; set; } = null!;

    public virtual DbSet<Database> Databases { get; set; } = null!;

    public virtual DbSet<Language> Languages { get; set; } = null!;

    public virtual DbSet<Project> Projects { get; set; } = null!;

    public virtual DbSet<Table> Tables { get; set; } = null!;

    public virtual DbSet<TableTranslation> TableTranslations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Document"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new ColumnMapping());
        modelBuilder.ApplyConfiguration(new ColumnTranslationMapping());
        modelBuilder.ApplyConfiguration(new DatabaseMapping());
        modelBuilder.ApplyConfiguration(new LanguageMapping());
        modelBuilder.ApplyConfiguration(new ProjectMapping());
        modelBuilder.ApplyConfiguration(new TableMapping());
        modelBuilder.ApplyConfiguration(new TableTranslationMapping());

        OnModelCreatingPartial(modelBuilder);
    }

    // ReSharper disable once PartialMethodWithSinglePart
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
