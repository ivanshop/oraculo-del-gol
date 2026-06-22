using Microsoft.EntityFrameworkCore;
using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Application.Responses;

namespace Oraculo.Infrastructure.Persistence.Repositories;

public class SeersRepository : ISeersRepository
{
    private readonly OraculoContext _context;

    public SeersRepository(OraculoContext context)
    {
        _context = context;
    }

    public async Task<List<RankingItemResponse>> GetRankingAsync()
    {
        var ranking = await _context.Predictions
            .Where(p => p.Match.HomeTeamGoals == p.Home && p.Match.AwayTeamGoals == p.Away && p.Match.IsEnded == 1)
            .GroupBy(p => p.Seer.Nick)
            .Select(g => new { Nick = g.Key, Points = g.Count(), PredictedAt = g.Average(p => p.CreatedAt.Value.Ticks) })
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.PredictedAt)
            .ToListAsync();

        return ranking
            .Select(r => new RankingItemResponse(r.Nick, r.Points))
            .ToList();
    }
}