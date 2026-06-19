namespace Oraculo.Application.Requests;

public record UpdatePredictionRequest(
    int Id,
    int MatchId,
    int UserId,
    int HomeGoals,
    int AwayGoals,
    DateTime CreatedAt
);