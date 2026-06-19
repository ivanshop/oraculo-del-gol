namespace Oraculo.Web.Models;

public class MatchPredictionModel
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime Scheduling { get; set; }
    public TeamModel Home { get; set; } = new();
    public TeamModel Away { get; set; } = new();
    public PredictionModel Prediction { get; set; } = new();
    public bool IsDone { get; set; }
}

public class TeamModel
{
    public string Id { get; set; } = string.Empty;
    public string Flag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? Goals { get; set; }
}

public class PredictionModel
{
    public int Id { get; set; }
    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }
    public DateTime? PredictedAt { get; set; }
}