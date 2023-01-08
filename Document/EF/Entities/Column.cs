using System;
using System.Collections.Generic;

namespace Document.EF.Entities;

public partial class Column
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public string? Name { get; set; }

    public string? DataType { get; set; }

    public int? Lenght { get; set; }

    public bool? IsNull { get; set; }

    public virtual ICollection<ColumnTranslation> ColumnTranslations { get; } = new List<ColumnTranslation>();

    public virtual Table Table { get; set; } = null!;
}
