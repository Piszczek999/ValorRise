using Riptide;

namespace MMO_Library.Server;

public class NewCharacterRequestEvent : EventArgs
{
    public Connection Client { get; }
    public string Name { get; }

    public NewCharacterRequestEvent(Connection client, string name)
    {
        Client = client;
        Name = name;
    }
}