using Oraculo.Web.Models;
using System.Net.Http.Json;

namespace Oraculo.Web.Services;

public class OraculoService
{
    private readonly HttpClient _httpClient;

    public OraculoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MatchModel>> GetMatchesTodayAsync(string telegramId, string date)
    {
        List<MatchModel> matches = new();
        try
        {
            var response = await _httpClient.GetAsync($"matches/{date}/seers/{telegramId}");
            if (response.IsSuccessStatusCode)
            {
                matches = await response.Content.ReadFromJsonAsync<List<MatchModel>>() ?? new List<MatchModel>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching matches: {ex.Message}");
        }        
        return matches;
    }

    public async Task<bool> UpdatePredictionsAsync(List<UpdatedPredictionModel> predictions)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"predictions", predictions);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating predictions: {ex.Message}");
        }
        return false;
    }
}
