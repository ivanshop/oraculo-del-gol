using System.Text.Json.Serialization;

namespace Oraculo.Application.Responses;

public record TelegramChat([property: JsonPropertyName("id")] long Id);
