namespace Oraculo.Application.Responses;

public record PredictionResponse(
    bool Success,
    string OfficialResult,
    string Prediction,
    DateTime? PredictionDate
);