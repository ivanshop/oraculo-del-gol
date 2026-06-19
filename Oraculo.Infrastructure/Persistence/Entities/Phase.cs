namespace Oraculo.Infrastructure.Persistence.Entities;

public class Phase
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Amount { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}