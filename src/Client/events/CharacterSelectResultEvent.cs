namespace MMOLibrary.Client;

public class CharacterSelectResultEvent : EventArgs
{
    public CharacterSelectResult Result { get; }

    public CharacterSelectResultEvent(CharacterSelectResult result)
    {
        Result = result;
    }
}