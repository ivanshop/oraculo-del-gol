namespace Oraculo.Web.Models;

public record UpdatedPredictionModel(
    int Id,
    int MatchId,
    string UserId,
    int HomeGoals,
    int AwayGoals,
    DateTime CreatedAt
);
