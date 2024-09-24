using Riptide;

namespace MMO_Library.Server;

public class NewCharacterRequestEvent : EventArgs
{
    public Connection Connection { get; }
    public string Name { get; }

    public NewCharacterRequestEvent(Connection connection, string name)
    {
        Connection = connection;
        Name = name;
    }
}