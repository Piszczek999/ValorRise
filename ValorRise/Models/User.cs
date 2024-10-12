using MongoDB.Bson;

namespace ValorRise.Models;

public class User
{
    public int FailedLoginAttempts { get; set; } = 0;
    public DateTime? LockoutEnd { get; set; }

    public ObjectId Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public string[] Roles { get; set; }
    public DateTime CreatedAt { get; set; }
}
