
using System.Collections.Generic;
using pokedex.Models;

namespace pokedex.Services
{
    public interface IPokemonService
    {
        List<Pokemon> GetPokemons();
        Pokemon GetPokemonById(string id);
        Pokemon GetPokemonByName(string name);
        Pokemon AddPokemon(Pokemon newPokemon);
        Pokemon UpdatePokemon(string id, Pokemon updatedPokemon);
        bool DeletePokemon(string id);
        
        // New method to support checking MongoDB collections
        List<string> GetCollections();
    }
}