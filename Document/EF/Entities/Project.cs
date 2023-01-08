namespace Document.EF.Entities;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Database> Databases { get; } = new List<Database>();
}
