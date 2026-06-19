using Microsoft.AspNetCore.Mvc;
using Oraculo.Api.Exntesions;
using Oraculo.Application.Interfaces;
using Oraculo.Application.Requests;

namespace Oraculo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PredictionsController : ControllerBase
{
    private readonly IUpdatePredictionUseCase _updatePredictionUseCase;
    private readonly IGetPredictionIdUseCase _getPredictionIdUseCase;

    public PredictionsController(IGetPredictionIdUseCase getPredictionIdUseCase,
        IUpdatePredictionUseCase updatePredictionUseCase)
    {
        _getPredictionIdUseCase = getPredictionIdUseCase;
        _updatePredictionUseCase = updatePredictionUseCase;
    }

    [HttpGet("matches/{matchId}/seers/{seerId}", Name = "GetPredictions")]
    public async Task<IActionResult> Get(int matchId, int seerId)
    {
        var response = await _getPredictionIdUseCase.ExecuteAsync(matchId, seerId);
        return response.ToHttpResult();
    }

    [HttpPost(Name = "AddPrediction")]
    public async Task<IActionResult> AddPredictionsAsync(List<UpdatePredictionRequest> request)
    {
        var response = await _updatePredictionUseCase.ExecuteAsync(request);
        return response.ToHttpResult();
    }
}