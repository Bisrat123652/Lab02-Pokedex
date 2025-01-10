using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using pokedex.Models;
using pokedex.Services;

namespace pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // This will map to "api/pokemon"
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            // Inject the service into the controller
            _pokemonService = pokemonService;
        }

        // GET: api/pokemon
        [HttpGet]
        public ActionResult<List<Pokemon>> Get()
        {
            var pokemons = _pokemonService.GetPokemons();
            return Ok(pokemons); // Return 200 OK with the list of Pokémon
        }

        // GET: api/pokemon/test-connection
        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                var collections = _pokemonService.GetCollections(); // Fetch collections
                return Ok(new { message = "MongoDB is connected successfully!", collections });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET: api/pokemon/{id}
        [HttpGet("{id}")]
        public ActionResult GetPokemon(string id)
        {
            try
            {
                var pokemon = _pokemonService.GetPokemonById(id);
                return Ok(pokemon); // Return 200 OK with the Pokémon
            }
            catch (Exception)
            {
                return NotFound("Pokemon not found"); // Return 404 if not found
            }
        }

        // GET: api/pokemon/search/{name}
        [HttpGet("search/{name}")]
        public ActionResult<Pokemon> GetPokemonByName(string name)
        {
            var pokemon = _pokemonService.GetPokemonByName(name);
            if (pokemon == null)
            {
                return NotFound("Pokemon not found"); // Return 404 if not found
            }
            return Ok(pokemon); // Return 200 OK with the Pokémon
        }

        // POST: api/pokemon
        [HttpPost]
        public ActionResult<Pokemon> AddPokemon([FromBody] Pokemon newPokemon)
        {
            try
            {
                var addedPokemon = _pokemonService.AddPokemon(newPokemon);
                return Ok(addedPokemon);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // PUT: api/pokemon/{id}
        [HttpPut("{id}")]
        public ActionResult<Pokemon> UpdatePokemon(string id, [FromBody] Pokemon updatedPokemon)
        {
            if (updatedPokemon == null)
            {
                return BadRequest("Invalid Pokémon data"); // Return 400 if the input is invalid
            }

            var result = _pokemonService.UpdatePokemon(id, updatedPokemon);
            if (result == null)
            {
                return NotFound("Pokemon not found"); // Return 404 if not found
            }
            return Ok(result); // Return 200 OK with the updated Pokémon
        }

        // DELETE: api/pokemon/{id}
        [HttpDelete("{id}")]
        public ActionResult<bool> DeletePokemon(string id)
        {
            var result = _pokemonService.DeletePokemon(id);
            if (!result)
            {
                return NotFound("Pokemon not found"); // Return 404 if not found
            }
            return Ok(result); // Return 200 OK if deleted
        }
    }
}