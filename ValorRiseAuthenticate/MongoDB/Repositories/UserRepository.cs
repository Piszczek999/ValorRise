using MongoDB.Bson;
using MongoDB.Driver;
using ValorRise.Models;

namespace ValorRiseAuthenticate.MongoDB.Repositories;

public class UserRepository
{
    private readonly MongoDBContext _context;

    public UserRepository(MongoDBContext context)
    {
        _context = context;
        SetLogoutUsers();
    }

    public async void SetLogoutUsers()
    {
        var update = Builders<User>.Update.Set(u => u.IsLogged, false);
        await _context.Users.UpdateManyAsync(user => user.IsLogged, update);
    }

    public async Task<List<User>> GetAllAsync() =>
        await _context.Users.Find(user => true).ToListAsync();

    public async Task<User> GetByIdAsync(ObjectId id) =>
        await _context.Users.Find(user => user.Id == id).FirstOrDefaultAsync();

    public async Task<User> GetByUsernameAsync(string username) =>
        await _context.Users.Find(user => user.Username == username).FirstOrDefaultAsync();

    public async Task CreateAsync(User user) =>
        await _context.Users.InsertOneAsync(user);

    public async Task UpdateAsync(User user) =>
        await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);

    public async Task DeleteAsync(ObjectId id) =>
        await _context.Users.DeleteOneAsync(u => u.Id == id);

    public async Task<bool> LogoutAsync(ObjectId id)
    {
        var update = Builders<User>.Update.Set(u => u.IsLogged, false);
        var result = await _context.Users.UpdateOneAsync(u => u.Id == id, update);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}
