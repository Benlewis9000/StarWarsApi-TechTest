using Microsoft.Extensions.Logging;
using NSubstitute;
using StarWarsApi.Services;

namespace StarWarsApi.Test.Services;

internal class HttpCharacterClientTests
{
    private ILogger<HttpCharacterClient> _logger;


    [SetUp]
    public void Init()
    {
        _logger = Substitute.For<ILogger<HttpCharacterClient>>();
    }

    [Test]
    public async Task Fetch_SuccessfullyFetchesCharacters()
    {
        ICharacterClient client = new HttpCharacterClient(_logger);
        var characters = (await client.FetchAsync()).ToList();
        Assert.That(characters.Count(), Is.EqualTo(10));
        var luke = characters.First();
        Assert.That(luke.Name, Is.EqualTo("Luke Skywalker"));
        Assert.That(luke.BirthYear, Is.EqualTo("19BBY"));
        Assert.That(luke.HomeWorld, Is.EqualTo("Tatooine"));
        Assert.That(luke.FilmCount, Is.EqualTo(4));
    }
}