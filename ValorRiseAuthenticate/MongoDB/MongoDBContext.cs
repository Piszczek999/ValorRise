using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ValorRise.Models;

namespace ValorRiseAuthenticate.MongoDB;

public class MongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Character> Characters => _database.GetCollection<Character>("characters");
    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<CharacterInfo> CharacterInfos => _database.GetCollection<CharacterInfo>("character_infos");
}

public class MongoDBSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}