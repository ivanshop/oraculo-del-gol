namespace Oraculo.Web.Models;

public record UpdatedPredictionModel(
    int Id,
    int MatchId,
    string UserId,
    string HomeTeamIsoCode,
    string AwayTeamIsoCode,
    int HomeGoals,
    int AwayGoals,
    string Username,
    DateTime CreatedAt
);
