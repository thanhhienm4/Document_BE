using System;
using System.Collections.Generic;

namespace Document.EF.Entities;

public partial class Table
{
    public int Id { get; set; }

    public int? DatabaseId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Column> Columns { get; } = new List<Column>();

    public virtual Database? Database { get; set; }

    public virtual ICollection<TableTranslation> TableTranslations { get; } = new List<TableTranslation>();
}
