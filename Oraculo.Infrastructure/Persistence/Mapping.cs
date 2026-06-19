using Oraculo.Application.Requests;
using Oraculo.Application.Responses;
using Oraculo.Infrastructure.Persistence.Entities;

namespace Oraculo.Infrastructure.Persistence;

public class Mapping
{
    public static PredictionResponse? ToDto(Prediction? prediction, List<Country> countries)
    {
        if (prediction is null) return null;
        var homeTeam = countries.First(c => c.Id == prediction.Match.HomeTeam).Name;
        var homeGoals = prediction.Match.HomeTeamGoals.Value;
        var awayTeam = countries.First(c => c.Id == prediction.Match.AwayTeam).Name;
        var awayGoals = prediction.Match.AwayTeamGoals.Value;

        var matchResult = homeTeam + " " + homeGoals + " - " + awayGoals + " " + awayTeam;
        var predictionScore = homeTeam + " " + prediction.Home + " - " + prediction.Away + " " + awayTeam;

        if (prediction.Home is null && prediction.Away is null) predictionScore = "No predictions";
        return new PredictionResponse(predictionScore == matchResult, matchResult, predictionScore,
            prediction.CreatedAt);
    }

    public static List<MatchResponse> ToDto(List<Match> matches, List<Country> countries)
    {
        List<MatchResponse> matchesToday = new();
        if (matches.Any())
            foreach (var match in matches)
                matchesToday.Add(new MatchResponse(
                    match.Id,
                    match.Predictions.FirstOrDefault(p => p.MatchId == match.Id)?.Id ?? 0,
                    match.HomeTeam,
                    countries.First(c => c.Id == match.HomeTeam).Code,
                    countries.First(c => c.Id == match.HomeTeam).Name,
                    match.HomeTeamGoals,
                    match.Predictions.FirstOrDefault(p => p.MatchId == match.Id)?.Home,
                    match.AwayTeam,
                    countries.First(c => c.Id == match.AwayTeam).Code,
                    countries.First(c => c.Id == match.AwayTeam).Name,
                    match.AwayTeamGoals,
                    match.Predictions.FirstOrDefault(p => p.MatchId == match.Id)?.Away,
                    match.ScheduleAt,
                    match.Predictions.FirstOrDefault(p => p.MatchId == match.Id)?.CreatedAt,
                    match.HomeTeamGoals is not null
                ));

        return matchesToday;
    }

    //public static List<Prediction> ToEntity(List<UpdatePredictionRequest> predictions)
    //{
    //    List<Prediction> entities = new();
    //    if (predictions.Any())
    //        foreach (var prediction in predictions)
    //        {
    //            var entity = new Prediction
    //            {
    //                Id = prediction.Id,
    //                MatchId = prediction.MatchId,
    //                SeerId = prediction.UserId,
    //                Home = prediction.HomeGoals,
    //                Away = prediction.AwayGoals,
    //                CreatedAt = prediction.CreatedAt,
    //                TelegramMessageId = ""
    //            };
    //            entities.Add(entity);
    //        }

    //    return entities;
    //}
}