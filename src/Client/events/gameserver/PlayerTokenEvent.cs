namespace MMOLibrary.Client;

public class PlayerTokenEvent : EventArgs
{
    public string Token { get; }
    public Character Character { get; }

    public PlayerTokenEvent(string token, Character character)
    {
        Token = token;
        Character = character;
    }
}