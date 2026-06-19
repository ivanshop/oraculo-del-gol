namespace Oraculo.Infrastructure.Persistence.Entities;

public class Country
{
    public string Id { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string League { get; set; } = null!;
    public virtual ICollection<Match> MatchAwayTeamNavigations { get; set; } = new List<Match>();
    public virtual ICollection<Match> MatchHomeTeamNavigations { get; set; } = new List<Match>();
}