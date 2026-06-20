using System.Text.Json.Serialization;

namespace Oraculo.Application.Responses;

public record TelegramMessage(
    [property: JsonPropertyName("message_id")] int MessageId,
    [property: JsonPropertyName("chat")] TelegramChat Chat,
    [property: JsonPropertyName("from")] TelegramUser? From,
    [property: JsonPropertyName("text")] string? Text
);
