namespace MMOLibrary.Client;

public class InitLevelEvent : EventArgs
{
    public ushort MapId { get; }

    public InitLevelEvent(ushort mapId)
    {
        MapId = mapId;
    }
}