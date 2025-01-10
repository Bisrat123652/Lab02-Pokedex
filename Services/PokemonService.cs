using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using pokedex.Models;

namespace pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemons;

        public PokemonService(IMongoDatabase database)
        {
            _pokemons = database.GetCollection<Pokemon>("Pokemons");
        }

        // Retrieve all Pokémon from the collection
        public List<Pokemon> GetPokemons()
        {
            return _pokemons.Find(pokemon => true).ToList();
        }

        // Retrieve a Pokémon by its ID
        public Pokemon GetPokemonById(string id)
        {
            return _pokemons.Find(pokemon => pokemon.Id == id).FirstOrDefault()
                ?? throw new Exception("Pokemon not found");
        }

        // Retrieve a Pokémon by its name
        public Pokemon GetPokemonByName(string name)
        {
            return _pokemons.Find(pokemon => pokemon.Name == name).FirstOrDefault()
                ?? throw new Exception("Pokemon not found");
        }

        // Add a new Pokémon to the collection
        public Pokemon AddPokemon(Pokemon newPokemon)
        {
            if (string.IsNullOrEmpty(newPokemon.Id))
            {
                newPokemon.Id = ObjectId.GenerateNewId().ToString(); // Generate a new ObjectId if none exists
            }

            _pokemons.InsertOne(newPokemon);
            return newPokemon;
        }

        // Update an existing Pokémon by its ID
        public Pokemon UpdatePokemon(string id, Pokemon updatedPokemon)
        {
            var result = _pokemons.ReplaceOne(pokemon => pokemon.Id == id, updatedPokemon);
            if (result.MatchedCount == 0)
            {
                throw new Exception("Pokemon not found");
            }
            return updatedPokemon;
        }

        // Delete a Pokémon by its ID
        public bool DeletePokemon(string id)
        {
            var result = _pokemons.DeleteOne(pokemon => pokemon.Id == id);
            return result.DeletedCount > 0;
        }

        // Get all collection names from the database
        public List<string> GetCollections()
        {
            return _pokemons.Database.ListCollectionNames().ToList();
        }
    }
}