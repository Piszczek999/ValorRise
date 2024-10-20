using MongoDB.Bson;
using MongoDB.Driver;
using ValorRise.Models;

namespace ValorRiseAuthenticate.MongoDB.Repositories;

public class CharacterRepository
{
    private readonly MongoDBContext _context;

    public CharacterRepository(MongoDBContext context)
    {
        _context = context;
    }

    public async Task<List<Character>> GetAllAsync() =>
        await _context.Characters.Find(charac => true).ToListAsync();

    public async Task<Character> GetByIdAsync(ObjectId id) =>
        await _context.Characters.Find(charac => charac.Id == id).FirstOrDefaultAsync();

    public async Task<Character> GetByNameAsync(string name) =>
        await _context.Characters.Find(charac => charac.Name == name).FirstOrDefaultAsync();

    public async Task CreateAsync(Character character) =>
        await _context.Characters.InsertOneAsync(character);

    public async Task UpdateAsync(Character character) =>
        await _context.Characters.ReplaceOneAsync(c => c.Id == character.Id, character);

    public async Task DeleteAsync(ObjectId id) =>
        await _context.Characters.DeleteOneAsync(c => c.Id == id);
}
