using System.Text.Json.Serialization;

namespace Oraculo.Application.Responses;

public record TelegramUser(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("first_name")] string FirstName,
    [property: JsonPropertyName("username")] string? Username
);
