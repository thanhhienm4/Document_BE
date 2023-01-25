namespace Document.Data.EF.Entities;

public partial class Language
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<ColumnTranslation> ColumnTranslations { get; } = new List<ColumnTranslation>();

    public virtual ICollection<TableTranslation> TableTranslations { get; } = new List<TableTranslation>();
}
