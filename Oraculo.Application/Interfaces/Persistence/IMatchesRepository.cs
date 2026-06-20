using Oraculo.Application.Responses;

namespace Oraculo.Application.Interfaces.Persistence;

public interface IMatchesRepository
{
    Task<List<MatchResponse>> GetMatchesByDate(string userTelegramId, DateTime date);
}