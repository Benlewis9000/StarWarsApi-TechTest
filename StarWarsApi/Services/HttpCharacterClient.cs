using Newtonsoft.Json;
using StarWarsApi.Model;

namespace StarWarsApi.Services;

public class HttpCharacterClient : ICharacterClient
{
    private readonly ILogger _logger;
    private readonly Uri _swapiEndpoint = new Uri("https://swapi.dev/api/");

    public HttpCharacterClient(ILogger<HttpCharacterClient> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<Character>> FetchAsync()
    {
        Uri peopleEndpoint = new Uri(_swapiEndpoint, "people");

        // Get character data from SWAPI
        using var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(peopleEndpoint);
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        List<CharacterResponse> characterResponses = JsonConvert.DeserializeObject<ResponseRoot<CharacterResponse>>(json).Results;

        // Fetch homeworld data and update character info in turn
        foreach (CharacterResponse character in characterResponses)
        {
            response = await client.GetAsync(character.Homeworld);
            response.EnsureSuccessStatusCode();
            json = await response.Content.ReadAsStringAsync();
            character.Homeworld = JsonConvert.DeserializeObject<PlanetResponse>(json).Name;
        }

        return characterResponses.Select(r => new Character
            { Name = r.Name, BirthYear = r.BirthYear, HomeWorld = r.Homeworld, FilmCount = r.Films.Count });
    }

    // Extract only what is needed from JSON responses
    private class ResponseRoot<T>
    {
        public List<T> Results { get; set; }
    }

    private class CharacterResponse
    {
        public string Name { get; set; }
        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }
        public string Homeworld { get; set; }
        public List<string> Films { get; set; }
    }

    private class PlanetResponse
    {
        public string Name { get; set; }
    }
}
