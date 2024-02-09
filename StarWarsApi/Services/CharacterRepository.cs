using StarWarsApi.Model;

namespace StarWarsApi.Services;

public class CharacterRepository : ICharacterRepository
{
    private readonly ILogger _logger;
    private readonly ICharacterClient _characterClient;
    private List<Character> _characters = new();
    private bool _isInitialised;

    public CharacterRepository(ILogger<CharacterRepository> logger, ICharacterClient characterClient)
    {
        _characterClient = characterClient;
        _logger = logger;
    }

    public async Task<IEnumerable<Character>> GetAll()
    {
        if (!_isInitialised)
        {
            await Update();
        }
        return _characters;
    }

    public async Task Update()
    {
        try
        {
            _characters = (await _characterClient.FetchAsync()).ToList();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch characters when updating repository");
        }
        _isInitialised = true;
    }
}