namespace Oraculo.Infrastructure.Persistence.Entities;

public class Match
{
    public int Id { get; set; }

    public int PhaseId { get; set; }

    public DateTime ScheduleAt { get; set; }

    public string HomeTeam { get; set; } = null!;

    public int? HomeTeamGoals { get; set; }

    public string AwayTeam { get; set; } = null!;

    public int? AwayTeamGoals { get; set; }

    public string? Group { get; set; }

    public int? IsEnded { get; set; }

    public virtual Country AwayTeamNavigation { get; set; } = null!;

    public virtual Country HomeTeamNavigation { get; set; } = null!;

    public virtual Phase Phase { get; set; } = null!;

    public virtual ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}