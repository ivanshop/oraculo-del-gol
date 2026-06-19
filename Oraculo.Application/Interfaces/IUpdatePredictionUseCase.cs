using Oraculo.Application.Requests;
using Oraculo.Application.Responses;

namespace Oraculo.Application.Interfaces;

public interface IUpdatePredictionUseCase
{
    public Task<Result<SimpleResponse>> ExecuteAsync(List<UpdatePredictionRequest> request);
}