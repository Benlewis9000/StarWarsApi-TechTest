using Microsoft.AspNetCore.Mvc;
using StarWarsApi.Services;

namespace StarWarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var characters = await _characterRepository.GetAll();
            if (!characters.Any())
            {
                return NoContent();
            }

            return Ok(characters);
        }
    }
}
