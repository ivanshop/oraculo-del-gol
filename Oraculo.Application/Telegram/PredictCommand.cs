using Oraculo.Application.Interfaces.Telegram;
using Oraculo.Application.Interfaces.TelegramBot;

namespace Oraculo.Application.Telegram;

public class PredictCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _botClient;

    public PredictCommand(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public string CommandName => "/today";

    public async Task ExecuteAsync(long chatId, string username)
    {
        string mensaje = $"¡Hola @{username}! 🔮 ¿Listo para ver más allá de lo evidente? Abre el Oráculo del Gol aquí abajo:";
        await _botClient.SendButtonMessageAsync(chatId, mensaje, "🔮 Abrir Oráculo", "https://ivanshop.github.io/oraculo-del-gol/");
    }
}
