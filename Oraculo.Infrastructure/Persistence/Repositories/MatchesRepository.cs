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

    public async Task<List<MatchResponse>> GetMatchesByDate(string userTelegramId, DateTime date)
    {
        var startDate = date.Date.AddHours(5);
        var endDate = startDate.AddHours(25);
        var countries = await _context.Countries.ToListAsync();
        var matches = await _context
            .Matches
            .Include(m => m.Predictions.Where(p => p.Seer.TelegramId == userTelegramId))
                .ThenInclude(p => p.Seer)
            .Where(m => m.ScheduleAt >= startDate && m.ScheduleAt <= endDate
                && m.Predictions.Any(p => p.Seer.TelegramId == userTelegramId))
            .ToListAsync();
        return Mapping.ToDto(matches, countries);
    }
}