using TestWebApi.Model;

namespace TestWebApi.Services;

public interface ICharacterClient
{
    /// <summary>
    /// Build <see cref="Character"/> models with data fetched from SWAPI
    /// </summary>
    /// <returns>Characters from SWAPI data</returns>
    Task<IEnumerable<Character>> FetchAsync();
}