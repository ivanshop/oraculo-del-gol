namespace Oraculo.Application.Interfaces.Telegram;

public interface ITelegramCommand
{
    string CommandName { get; }
    Task ExecuteAsync(long chatId, string username);
}
