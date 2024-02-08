using NSubstitute;
using NUnit.Framework.Legacy;
using StarWarsApi.Model;
using StarWarsApi.Services;

namespace StarWarsApi.Test.Services;

internal class CharacterRepositoryTests
{
    [Test]
    public async Task GetAll_ReturnsCharacters()
    {
        ICharacterClient characterClient = Substitute.For<ICharacterClient>();
        Character character = new() { Name = "Bob", BirthYear = "1999", HomeWorld = "Earth", FilmCount = 6 };
        characterClient.FetchAsync().Returns(new List<Character>() { character, character });

        ICharacterRepository repository = new CharacterRepository(characterClient);
        var result = (await repository.GetAll()).ToList();
        Assert.That(result.Count(), Is.EqualTo(2));
        StringAssert.AreEqualIgnoringCase("Bob", result.First().Name);
    }

    [Test]
    public async Task GetAll_OnlyFetchesDataOnce()
    {
        ICharacterClient characterClient = Substitute.For<ICharacterClient>();
        Character character = new() { Name = "Bob", BirthYear = "1999", HomeWorld = "Earth", FilmCount = 6 };
        characterClient.FetchAsync().Returns(new List<Character>() { character, character });

        ICharacterRepository repository = new CharacterRepository(characterClient);
        await repository.GetAll();
        await characterClient.Received(1).FetchAsync();
        await repository.GetAll();
        characterClient.ClearReceivedCalls();
        await characterClient.DidNotReceive().FetchAsync();
    }
}