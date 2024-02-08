using StarWarsApi.Model;

namespace StarWarsApi.Services;

public class CharacterRepository : ICharacterRepository
{
    private readonly ICharacterClient _characterClient;
    private List<Character> _characters = new();
    private bool _isInitialised = false;

    public CharacterRepository(ICharacterClient characterClient)
    {
        _characterClient = characterClient;
    }

    public async Task<IEnumerable<Character>> GetAll()
    {
        if (!_isInitialised)
        {
            await Update();
            _isInitialised = true;
        }
        return _characters;
    }

    public async Task Update()
    {
        _characters = (await _characterClient.FetchAsync()).ToList();
    }
}