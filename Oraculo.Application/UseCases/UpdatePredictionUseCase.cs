using Oraculo.Application.Interfaces;
using Oraculo.Application.Interfaces.Persistence;
using Oraculo.Application.Interfaces.TelegramBot;
using Oraculo.Application.Requests;
using Oraculo.Application.Responses;
using System.Text;

namespace Oraculo.Application.UseCases;

public class UpdatePredictionUseCase : IUpdatePredictionUseCase
{
    private readonly IPredictionsRepository _predictionsRepository;
    private readonly ITelegramBotClient _telegramBot;
    private readonly long _grupoId = -5561189421;
    public static readonly Dictionary<string, string> Flags = new(StringComparer.OrdinalIgnoreCase)
    {
        ["mx"] = "🇲🇽", ["us"] = "🇺🇸", ["ca"] = "🇨🇦", ["ar"] = "🇦🇷", ["br"] = "🇧🇷", ["uy"] = "🇺🇾", ["ec"] = "🇪🇨", ["py"] = "🇵🇾",
        ["fr"] = "🇫🇷", ["gb-eng"] = "🏴", ["es"] = "🇪🇸", ["pt"] = "🇵🇹", ["it"] = "🇮🇹", ["de"] = "🇩🇪", ["nl"] = "🇳🇱", ["be"] = "🇧🇪",
        ["hr"] = "🇭🇷", ["at"] = "🇦🇹", ["se"] = "🇸🇪", ["no"] = "🇳🇴", ["cz"] = "🇨🇿", ["gb-sct"] = "🏴", ["ba"] = "🇧🇦", ["ma"] = "🇲🇦",
        ["sn"] = "🇸🇳", ["eg"] = "🇪🇬", ["ci"] = "🇨🇮", ["dz"] = "🇩🇿", ["gh"] = "🇬🇭", ["tn"] = "🇹🇳", ["za"] = "🇿🇦", ["ao"] = "🇦🇴",
        ["cv"] = "🇨🇻", ["jp"] = "🇯🇵", ["kr"] = "🇰🇷", ["ir"] = "🇮🇷", ["sa"] = "🇸🇦", ["au"] = "🇦🇺", ["qa"] = "🇶🇦", ["iq"] = "🇮🇶",
        ["cn"] = "🇨🇳", ["jo"] = "🇯🇴", ["nz"] = "🇳🇿", ["hn"] = "🇭🇳", ["ht"] = "🇭🇹", ["cw"] = "🇨🇼", ["ch"] = "🇨🇭", ["tr"] = "🇹🇷",
        ["cd"] = "🇨🇩", ["pa"] = "🇵🇦", ["co"] = "🇨🇴", ["uz"] = "🇺🇿"
    };

    public UpdatePredictionUseCase(IPredictionsRepository predictionsRepository, ITelegramBotClient telegramBot)
    {
        _predictionsRepository = predictionsRepository;
        _telegramBot = telegramBot;
    }

    public async Task<Result<SimpleResponse>> ExecuteAsync(List<UpdatePredictionRequest> request)
    {
        var result = new SimpleResponse(false);
        string usuario = !string.IsNullOrEmpty(request.First().Username) ? $"@{request.First().Username}" : "Un usuario";
        if (request.Any())
        {
            var isSuccess = await _predictionsRepository.UpdatePredictionAsync(request);
            if (isSuccess)
            {
                result = new SimpleResponse(isSuccess);
                var sb = new StringBuilder();
                sb.AppendLine($"🔮 *¡Predicción Registrada!* 🏟️");
                sb.AppendLine($"{usuario} ha pronosticado:");
                sb.AppendLine();
                foreach (var prediction in request)
                {
                    string homeFlag = Flags.ContainsKey(prediction.HomeTeamIsoCode) ? Flags[prediction.HomeTeamIsoCode] : "🏳️";
                    string awayFlag = Flags.ContainsKey(prediction.AwayTeamIsoCode) ? Flags[prediction.AwayTeamIsoCode] : "🏳️";
                    sb.AppendLine($"{homeFlag} *{prediction.HomeGoals} - {prediction.AwayGoals}* {awayFlag}");
                }
                sb.AppendLine();
                sb.AppendLine($"_¡Buena suerte!_");
                await _telegramBot.SendMessageAsync(_grupoId, sb.ToString());
                return Result<SimpleResponse>.Success(CodeResponseType.Created, result);
            }
            return Result<SimpleResponse>.Failure(CodeResponseType.Conflict, "Predictions not updated.");
        }
        return Result<SimpleResponse>.Failure(CodeResponseType.BadRequest, "Predictions are required.");
    }

    private string GetFlagEmojiByIsoCode(string countryCode)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            return string.Empty;

        countryCode = countryCode.ToUpperInvariant();

        return string.Concat(countryCode.Select(c =>
            char.ConvertFromUtf32(0x1F1E6 + (c - 'A'))));
    }
}