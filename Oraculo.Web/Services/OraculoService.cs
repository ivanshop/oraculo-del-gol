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

    public async Task<List<MatchModel>> GetMatchesToday(string date)
    {
        List<MatchModel> matches = new();
        try
        {
            var response = await _httpClient.GetAsync($"matches/{date}");
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
}
