using System.Text.Json.Serialization;

namespace Oraculo.Application.Responses;

public record TelegramUpdate(
    [property: JsonPropertyName("update_id")] int UpdateId,
    [property: JsonPropertyName("message")] TelegramMessage? Message
);
