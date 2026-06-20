namespace Oraculo.Application.Interfaces.TelegramBot;

public interface ITelegramBotClient
{
    Task SendMessageAsync(long chatId, string message);
    Task SendButtonMessageAsync(long chatId, string message, string buttonText, string buttonUrl);
}
