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
        string mensaje = $"🔮 ¿Listos para ver más allá de lo evidente? Tap al Oráculo del Gol aquí abajo:";
        await _botClient.SendButtonMessageAsync(chatId, mensaje, "🔮 Abrir Oráculo", "t.me/OraculoDelGolBot/play");
    }
}
