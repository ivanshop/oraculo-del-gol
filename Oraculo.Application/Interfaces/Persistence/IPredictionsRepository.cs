using Oraculo.Application.Requests;
using Oraculo.Application.Responses;

namespace Oraculo.Application.Interfaces.Persistence;

public interface IPredictionsRepository
{
    Task<PredictionResponse?> GetPredictionByMatchAndSeer(int matchId, int seerId);
    Task<bool> UpdatePredictionAsync(List<UpdatePredictionRequest> predictions);
}