using Oraculo.Application.Interfaces;
using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Application.Requests;
using Oraculo.Application.Responses;

namespace Oraculo.Application.UseCases;

public class UpdatePredictionUseCase : IUpdatePredictionUseCase
{
    private readonly IPredictionsRepository _predictionsRepository;

    public UpdatePredictionUseCase(IPredictionsRepository predictionsRepository)
    {
        _predictionsRepository = predictionsRepository;
    }

    public async Task<Result<SimpleResponse>> ExecuteAsync(List<UpdatePredictionRequest> request)
    {
        var result = new SimpleResponse(false);
        if (request.Any())
        {
            var isSuccess = await _predictionsRepository.UpdatePredictionAsync(request);
            if (isSuccess)
            {
                result = new SimpleResponse(isSuccess);
                return Result<SimpleResponse>.Success(CodeResponseType.Created, result);
            }
            return Result<SimpleResponse>.Failure(CodeResponseType.Conflict, "Predictions not updated.");
        }
        return Result<SimpleResponse>.Failure(CodeResponseType.BadRequest, "Predictions are required.");
    }
}