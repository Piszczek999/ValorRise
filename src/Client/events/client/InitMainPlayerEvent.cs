namespace MMOLibrary.Client;

public class InitMainPlayerEvent : EventArgs
{
    public Character Character { get; }

    public InitMainPlayerEvent(Character character)
    {
        Character = character;
    }
}