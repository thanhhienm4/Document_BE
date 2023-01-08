namespace Document.EF.Entities;

public partial class TableTranslation
{
    public int TableId { get; set; }

    public string LanguageId { get; set; } = null!;

    public string? Summary { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}
