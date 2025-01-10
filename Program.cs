using pokedex.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Connection
var mongoClient = new MongoClient("mongodb://localhost:27017"); // Local MongoDB URI
var database = mongoClient.GetDatabase("PokedexDatabase");     // Database name

// Register MongoDB services
builder.Services.AddSingleton(mongoClient);
builder.Services.AddSingleton(database);

// Add controllers and other services
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonService, PokemonService>();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();