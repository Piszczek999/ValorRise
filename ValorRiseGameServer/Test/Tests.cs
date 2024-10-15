using MongoDB.Bson;
using ValorRise;
using ValorRiseGameServer;
using ValorRiseGameServer.Entities;
using ValorRiseGameServer.Events;
using Xunit;

public class Tests
{
    public Tests()
    {
        ValorServer.Init();
    }

    [Fact]
    public void Test()
    {
        ValorServer.GlobalEventNode.AddListener<PlayerJoinEvent>((e) => { });
        ValorServer.GlobalEventNode.Invoke(new PlayerJoinEvent(new Player(null, ObjectId.GenerateNewId())));
    }
}
