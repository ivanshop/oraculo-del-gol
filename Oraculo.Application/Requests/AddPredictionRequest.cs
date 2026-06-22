namespace Oraculo.Application.Requests;

public record UpdatePredictionRequest(
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