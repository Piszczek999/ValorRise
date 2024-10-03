namespace MMOLibrary.Client;

public class CharactersInfoDBResponseEvent : EventArgs
{
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public CharacterInfo[] CharacterInfos { get; }

    public CharactersInfoDBResponseEvent(ushort gatewayId, ushort clientId, CharacterInfo[] characterInfos)
    {
        GatewayId = gatewayId;
        ClientId = clientId;
        CharacterInfos = characterInfos;
    }
}