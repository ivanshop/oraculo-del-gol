using Oraculo.Application.Responses;

namespace Oraculo.Application.Interfaces.Persistence;

public interface ISeersRepository
{
    Task<List<RankingItemResponse>> GetRankingAsync();
}
