using Microsoft.EntityFrameworkCore;
using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Application.Requests;
using Oraculo.Application.Responses;
using Oraculo.Infrastructure.Persistence.Entities;

namespace Oraculo.Infrastructure.Persistence.Repositories;

public class PredictionsRepository : IPredictionsRepository
{
    private readonly OraculoContext _context;

    public PredictionsRepository(OraculoContext context)
    {
        _context = context;
    }

    public async Task<PredictionResponse?> GetPredictionByMatchAndSeer(int matchId, int seerId)
    {
        var countries = await _context.Countries.ToListAsync();
        var prediction = await _context
            .Predictions
            .Where(p => p.MatchId == matchId && p.SeerId == seerId)
            .Include(p => p.Match)
            .FirstOrDefaultAsync();
        return Mapping.ToDto(prediction, countries);
    }

    public async Task<bool> UpdatePredictionAsync(List<UpdatePredictionRequest> predictions)
    {
        var predictionIds = predictions.Select(p => p.Id).ToList();
        var existingPredictions = await _context.Predictions.Where(p => predictionIds.Contains(p.Id)).ToListAsync();

        if (existingPredictions.Count != predictions.Count)
            return false;

        foreach (var prediction in predictions)
        {
            Prediction entity = existingPredictions.First(e => e.Id == prediction.Id);
            entity.Home = prediction.HomeGoals;
            entity.Away = prediction.AwayGoals;
            entity.CreatedAt = prediction.CreatedAt;
        }

        await _context.SaveChangesAsync();
        return true;
    }
}