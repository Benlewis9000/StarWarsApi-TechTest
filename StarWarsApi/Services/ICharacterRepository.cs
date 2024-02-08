using TestWebApi.Model;

namespace TestWebApi.Services;

public interface ICharacterRepository
{
    /// <summary>
    /// Get all <see cref="Character"/> instances stored in the repository
    /// </summary>
    /// <returns>All character models</returns>
    Task<IEnumerable<Character>> GetAll();

    Task Update();
}