using Oraculo.Application.Responses;

namespace Oraculo.Application.Interfaces;

public interface IGetPredictionIdUseCase
{
    public Task<Result<PredictionResponse?>> ExecuteAsync(int matchId, int seerId);
}