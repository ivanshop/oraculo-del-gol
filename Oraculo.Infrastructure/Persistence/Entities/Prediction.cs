namespace Oraculo.Infrastructure.Persistence.Entities;

public class Prediction
{
    public int Id { get; set; }

    public int MatchId { get; set; }

    public int SeerId { get; set; }

    public int? Home { get; set; }

    public int? Away { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? TelegramMessageId { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual Seer Seer { get; set; } = null!;
}