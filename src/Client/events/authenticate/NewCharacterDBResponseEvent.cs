using MongoDB.Bson;

namespace MMOLibrary.Client;

public class NewCharacterDBResponseEvent : EventArgs
{
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public NewCharacterResult Result { get; }

    public NewCharacterDBResponseEvent(ushort gatewayId, ushort clientId, NewCharacterResult result)
    {
        GatewayId = gatewayId;
        ClientId = clientId;
        Result = result;
    }
}