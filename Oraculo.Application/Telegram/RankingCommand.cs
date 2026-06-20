using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Application.Interfaces.Telegram;
using Oraculo.Application.Interfaces.TelegramBot;
using Oraculo.Application.Responses;

namespace Oraculo.Application.Telegram
{
    public class RankingCommand : ITelegramCommand
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ISeersRepository _seersRepository;

        public RankingCommand(ITelegramBotClient botClient, ISeersRepository seersRepository)
        {
            _botClient = botClient;
            _seersRepository = seersRepository;
        }

        public string CommandName => "/ranking";

        public async Task ExecuteAsync(long chatId, string username)
        {
            string message = "🏆 *Ranking del Oráculo del gol:*" + Environment.NewLine;
            List<RankingItemResponse> ranking = await _seersRepository.GetRankingAsync();
            if (ranking.Any())
            {
                foreach (var (index, itemRanking) in ranking.Index())
                {
                    message += $"{index + 1}. {itemRanking.Nick} - {itemRanking.Points} puntos" + Environment.NewLine;
                }
            }
            else
            {
                message = "Ranking no disponible. Intenta más tarde.";
            }            
            await _botClient.SendMessageAsync(chatId, message);
        }
    }
}
