namespace Oraculo.Application.Responses;

public record MatchResponse(
    int Id,
    int PredictionId,
    string HomeId,
    string HomeFlag,
    string Home,
    int? HomeGoals,
    int? HomePrediction,
    string AwayId,
    string AwayFlag,
    string Away,
    int? AwayGoals,
    int? AwayPrediction,
    DateTime MatchTime,
    DateTime? PredictionDate,
    bool IsDone
);