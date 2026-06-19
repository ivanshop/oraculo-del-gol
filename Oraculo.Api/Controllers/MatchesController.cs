using Microsoft.AspNetCore.Mvc;
using Oraculo.Api.Exntesions;
using Oraculo.Application.Interfaces;

namespace Oraculo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchesController : ControllerBase
{
    private readonly IGetMatchesIdUseCase _getMatchesIdUseCase;
    private readonly ILogger<MatchesController> _logger;

    public MatchesController(IGetMatchesIdUseCase getMatchesIdUseCase, ILogger<MatchesController> logger)
    {
        _logger = logger;
        _getMatchesIdUseCase = getMatchesIdUseCase;
    }

    [HttpGet("{date}", Name = "GetCurrentMatches")]
    public async Task<IActionResult> GetCurrentMatches([FromRoute] DateOnly? date)
    {
        var response = await _getMatchesIdUseCase.ExecuteAsync(date);
        return response.ToHttpResult();
    }
}