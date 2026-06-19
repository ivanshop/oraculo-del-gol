namespace Oraculo.Web.Models;

public record MatchModel(
    int Id,
    string HomeId,
    string HomeFlag,
    string Home,
    int? HomeGoals,
    string AwayId,
    string AwayFlag,
    string Away,
    int? AwayGoals,
    DateTime MatchTime,
    bool IsDone
);
