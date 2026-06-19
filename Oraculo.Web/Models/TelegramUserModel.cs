namespace Oraculo.Web.Models;

public record TelegramUserModel
{
    public long Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string LanguageCode { get; init; } = string.Empty;
}
