using Microsoft.AspNetCore.Mvc;
using Oraculo.Application.Interfaces.Telegram;
using Oraculo.Application.Responses;
using System.Text.Json;

namespace Oraculo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TelegramController : ControllerBase
{
    private readonly Dictionary<string, ITelegramCommand> _commands;
    private readonly ILogger<TelegramController> _logger;

    public TelegramController(IEnumerable<ITelegramCommand> commands, ILogger<TelegramController> logger)
    {
        _logger = logger;
        _commands = commands.ToDictionary(c => c.CommandName, c => c);
    }

    [HttpPost("webhook", Name = "Webhook")]
    public async Task<IActionResult> HandleUpdate([FromBody] TelegramUpdate update)
    {
        _logger.LogInformation("Received Telegram update: {Update}", JsonSerializer.Serialize(update));
        if (update?.Message?.Text is null)
        {
            return Ok();
        }

        var text = update.Message.Text.Trim();

        if (text.StartsWith("/"))
        {
            var command = text.Split(' ')[0].Split('@')[0].ToLower();
            var chatId = update.Message.Chat.Id;
            var username = update.Message.From?.Username ?? update.Message.From?.FirstName ?? "Usuario";

            if (_commands.TryGetValue(command, out var strategy))
            {
                await strategy.ExecuteAsync(chatId, username);
            }
        }

        return Ok();
    }
}