using Oraculo.Application.Interfaces;
using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Application.Responses;

namespace Oraculo.Application.UseCases;

public class GetMatchesUseCase : IGetMatchesIdUseCase
{
    private readonly IMatchesRepository _matchesRepository;

    public GetMatchesUseCase(IMatchesRepository matchesRepository)
    {
        _matchesRepository = matchesRepository;
    }

    public async Task<Result<List<MatchResponse>>> ExecuteAsync(DateOnly? date = null)
    {
        var currentDate = date is null ? DateTime.Now : date.Value.ToDateTime(TimeOnly.MinValue);
        var matches = await _matchesRepository.GetMatchesByDate(currentDate);
        if (matches.Any()) return Result<List<MatchResponse>>.Success(CodeResponseType.Ok, matches);
        return Result<List<MatchResponse>>.Failure(CodeResponseType.NotFound, "Not found matches.");
    }
}