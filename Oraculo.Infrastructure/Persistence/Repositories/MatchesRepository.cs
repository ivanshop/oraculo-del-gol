using Microsoft.EntityFrameworkCore;
using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Application.Responses;

namespace Oraculo.Infrastructure.Persistence.Repositories;

public class MatchesRepository : IMatchesRepository
{
    private readonly OraculoContext _context;

    public MatchesRepository(OraculoContext context)
    {
        _context = context;
    }

    public async Task<List<MatchResponse>> GetMatchesByDate(DateTime date)
    {
        var startDate = date.Date.AddHours(5);
        var endDate = startDate.AddHours(25);
        var countries = await _context.Countries.ToListAsync();
        var matches = await _context
            .Matches
            .Include(m => m.Predictions)
            .Where(m => m.ScheduleAt >= startDate && m.ScheduleAt <= endDate)
            .ToListAsync();
        return Mapping.ToDto(matches, countries);
    }
}