using MongoDB.Bson;

namespace MMOLibrary.Client;

public class RegisterDBResponseEvent : EventArgs
{
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public RegisterResult Result { get; }

    public RegisterDBResponseEvent(ushort gatewayId, ushort clientId, RegisterResult result)
    {
        GatewayId = gatewayId;
        ClientId = clientId;
        Result = result;
    }
}