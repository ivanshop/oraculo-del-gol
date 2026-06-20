using Oraculo.Application.Interfaces.TelegramBot;
using System.Net.Http.Json;

namespace Oraculo.Infrastructure.External
{
    public class TelegramBotClient : ITelegramBotClient
    {
        private readonly HttpClient _httpClient;
        private const string BotToken = "8917581300:AAFd8GxIY6pEW21Z4iLiG0vVvuiTjkzlDMQ";
        private readonly string _baseUrl = $"https://api.telegram.org/bot{BotToken}/sendMessage";

        public TelegramBotClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendMessageAsync(long chatId, string message)
        {
            var payload = new { chat_id = chatId, text = message, parse_mode = "Markdown" };
            await _httpClient.PostAsJsonAsync(_baseUrl, payload);
        }

        public async Task SendButtonMessageAsync(long chatId, string message, string buttonText, string buttonUrl)
        {
            var payload = new
            {
                chat_id = chatId,
                text = message,
                reply_markup = new
                {
                    inline_keyboard = new[] {
                    new[] { new { text = buttonText, url = buttonUrl } }
                }
                }
            };
            await _httpClient.PostAsJsonAsync(_baseUrl, payload);
        }
    }
}
