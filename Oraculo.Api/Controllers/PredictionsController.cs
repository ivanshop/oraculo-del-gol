using Microsoft.AspNetCore.Mvc;
using Oraculo.Api.Exntesions;
using Oraculo.Application.Interfaces;
using Oraculo.Application.Requests;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Oraculo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PredictionsController : ControllerBase
{
    private readonly ILogger<PredictionsController> _logger;
    private readonly IUpdatePredictionUseCase _updatePredictionUseCase;
    private readonly IGetPredictionIdUseCase _getPredictionIdUseCase;

    public PredictionsController(IGetPredictionIdUseCase getPredictionIdUseCase,
        IUpdatePredictionUseCase updatePredictionUseCase,
        ILogger<PredictionsController> logger)
    {
        _getPredictionIdUseCase = getPredictionIdUseCase;
        _updatePredictionUseCase = updatePredictionUseCase;
        _logger = logger;
    }

    [HttpGet("matches/{matchId}/seers/{seerId}", Name = "GetPredictions")]
    public async Task<IActionResult> Get(int matchId, int seerId)
    {
        _logger.LogInformation("Received request for predictions: {MatchId} for seer: {SeerId}", matchId, seerId);
        var response = await _getPredictionIdUseCase.ExecuteAsync(matchId, seerId);
        return response.ToHttpResult();
    }

    [HttpPost(Name = "UpdatePrediction")]
    public async Task<IActionResult> UpdatePredictionsAsync(List<UpdatePredictionRequest> request)
    {
        var response = await _updatePredictionUseCase.ExecuteAsync(request);
        return response.ToHttpResult();
    }
}