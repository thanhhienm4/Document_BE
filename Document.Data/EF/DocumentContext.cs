using Document.Data.EF.Entities;
using Document.Data.EF.Maps;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Document.Data.EF;

// ReSharper disable once PartialTypeWithSinglePart
public partial class DocumentContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    public DocumentContext(DbContextOptions<DocumentContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Column> Columns { get; set; } = null!;

    public virtual DbSet<ColumnTranslation> ColumnTranslations { get; set; } = null!;

    public virtual DbSet<Database> Databases { get; set; } = null!;

    public virtual DbSet<Language> Languages { get; set; } = null!;

    public virtual DbSet<Project> Projects { get; set; } = null!;

    public virtual DbSet<Table> Tables { get; set; } = null!;

    public virtual DbSet<TableTranslation> TableTranslations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var temp = _configuration.GetSection("ConnectionStrings");
        Console.WriteLine(temp.Value);
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DocumentDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ColumnMapping());
        modelBuilder.ApplyConfiguration(new ColumnTranslationMapping());
        modelBuilder.ApplyConfiguration(new DatabaseMapping());
        modelBuilder.ApplyConfiguration(new LanguageMapping());
        modelBuilder.ApplyConfiguration(new ProjectMapping());
        modelBuilder.ApplyConfiguration(new TableMapping());
        modelBuilder.ApplyConfiguration(new TableTranslationMapping());

        base.OnModelCreating(modelBuilder);
    }

    // ReSharper disable once PartialMethodWithSinglePart
    // ReSharper disable once UnusedMember.Local
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
