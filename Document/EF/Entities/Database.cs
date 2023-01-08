﻿using System;
using System.Collections.Generic;

namespace Document.EF.Entities;

public partial class Database
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Table> Tables { get; } = new List<Table>();
}
