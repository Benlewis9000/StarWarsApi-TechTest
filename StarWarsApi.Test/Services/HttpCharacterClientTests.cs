using NUnit.Framework.Legacy;
using StarWarsApi.Services;

namespace StarWarsApi.Test.Services;

internal class HttpCharacterClientTests
{
    [Test]
    public async Task Fetch_SuccessfullyFetchesCharacters()
    {
        ICharacterClient client = new HttpCharacterClient();
        var characters = (await client.FetchAsync()).ToList();
        Assert.That(characters.Count(), Is.EqualTo(10));
        var luke = characters.First();
        Assert.That(luke.Name, Is.EqualTo("Luke Skywalker"));
        Assert.That(luke.BirthYear, Is.EqualTo("19BBY"));
        Assert.That(luke.HomeWorld, Is.EqualTo("Tatooine"));
        Assert.That(luke.FilmCount, Is.EqualTo(4));
    }
}