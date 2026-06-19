using Oraculo.Application.Interfaces;
using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Application.Responses;

namespace Oraculo.Application.UseCases;

public class GetPredictionUseCase : IGetPredictionIdUseCase
{
    private readonly IPredictionsRepository _predictionsRepository;

    public GetPredictionUseCase(IPredictionsRepository predictionsRepository)
    {
        _predictionsRepository = predictionsRepository;
    }

    public async Task<Result<PredictionResponse?>> ExecuteAsync(int matchId, int seerId)
    {
        var prediction = await _predictionsRepository.GetPredictionByMatchAndSeer(matchId, seerId);
        if (prediction is null)
            return Result<PredictionResponse?>.Failure(CodeResponseType.NotFound, "Not found prediction");
        return Result<PredictionResponse?>.Success(CodeResponseType.Ok, prediction);
    }
}