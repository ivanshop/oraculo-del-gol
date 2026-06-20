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
        return await _context.Predictions
            .Where(p => p.Match.HomeTeamGoals == p.Home && p.Match.AwayTeamGoals == p.Away)
            .GroupBy(p => p.Seer.Nick)
            .Select(g => new RankingItemResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Points)
            .ToListAsync();
    }
}