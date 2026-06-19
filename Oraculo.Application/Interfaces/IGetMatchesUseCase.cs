using Oraculo.Application.Responses;

namespace Oraculo.Application.Interfaces;

public interface IGetMatchesIdUseCase
{
    public Task<Result<List<MatchResponse>>> ExecuteAsync(DateOnly? date = null);
}