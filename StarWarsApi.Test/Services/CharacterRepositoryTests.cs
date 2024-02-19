using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework.Legacy;
using StarWarsApi.Model;
using StarWarsApi.Services;

namespace StarWarsApi.Test.Services;

internal class CharacterRepositoryTests
{
    private ILogger<CharacterRepository> _logger;

    [SetUp]
    public void Init()
    {
        _logger = Substitute.For<ILogger<CharacterRepository>>();
    }

    [Test]
    public async Task GetAll_ReturnsCharacters()
    {
        ICharacterClient characterClient = Substitute.For<ICharacterClient>();
        Character character = new() { Name = "Bob", BirthYear = "1999", HomeWorld = "Earth", FilmCount = 6 };
        characterClient.FetchAsync().Returns(new List<Character>() { character, character });

        ICharacterRepository repository = new CharacterRepository(_logger, characterClient);
        var result = (await repository.GetAll()).ToList();
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.First().Name, Is.EqualTo("Bob"));
    }

    [Test]
    public async Task GetAll_OnlyFetchesDataOnce()
    {
        ICharacterClient characterClient = Substitute.For<ICharacterClient>();
        Character character = new() { Name = "Bob", BirthYear = "1999", HomeWorld = "Earth", FilmCount = 6 };
        characterClient.FetchAsync().Returns(new List<Character>() { character, character });

        ICharacterRepository repository = new CharacterRepository(_logger, characterClient);
        await repository.GetAll();
        await characterClient.Received(1).FetchAsync();
        characterClient.ClearReceivedCalls();
        await repository.GetAll();
        await characterClient.DidNotReceive().FetchAsync();
    }
}