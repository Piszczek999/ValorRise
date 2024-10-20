using MongoDB.Bson;
using MongoDB.Driver;
using ValorRise.Models;

namespace ValorRiseAuthenticate.MongoDB.Repositories;

public class CharacterInfoRepository
{
    private readonly MongoDBContext _context;

    public CharacterInfoRepository(MongoDBContext context)
    {
        _context = context;
    }

    public async Task<List<CharacterInfo>> GetAllAsync() =>
        await _context.CharacterInfos.Find(info => true).ToListAsync();

    public async Task<CharacterInfo> GetByIdAsync(ObjectId id) =>
        await _context.CharacterInfos.Find(info => info.Id == id).FirstOrDefaultAsync();

    public async Task<List<CharacterInfo>> GetAllByUserIdAsync(ObjectId userId) =>
        await _context.CharacterInfos.Find(info => info.UserId == userId).ToListAsync();

    public async Task CreateAsync(CharacterInfo info) =>
        await _context.CharacterInfos.InsertOneAsync(info);

    public async Task UpdateAsync(ObjectId id, CharacterInfo info) =>
        await _context.CharacterInfos.ReplaceOneAsync(i => i.Id == id, info);

    public async Task DeleteAsync(ObjectId id) =>
        await _context.CharacterInfos.DeleteOneAsync(i => i.Id == id);
}
