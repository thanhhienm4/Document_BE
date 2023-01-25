namespace Document.Data.EF.Entities;

public partial class ColumnTranslation
{
    public int ColumnId { get; set; }

    public string LanguageId { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public virtual Column Column { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}
