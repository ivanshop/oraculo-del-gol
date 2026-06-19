namespace Oraculo.Infrastructure.Persistence.Entities;

public class Seer
{
    public int Id { get; set; }

    public string TelegramId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Nick { get; set; }

    public virtual ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}