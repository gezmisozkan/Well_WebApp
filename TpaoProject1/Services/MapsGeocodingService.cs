using System.Net.Http;
using System.Threading.Tasks;

public class MapsGeocodingService
{
    private readonly HttpClient _httpClient;

    public MapsGeocodingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetGeocodingData(double latitude, double longitude, string apiKey)
    {
        string requestUrl = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={apiKey}";

        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        // Handle error cases here if needed.
        return null;
    }
}
