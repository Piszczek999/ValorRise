namespace ValorRise;

public static class MessageType
{
    public enum ToClient : ushort
    {
        // From Gateway
        LoginResponse = 1000,
        RegisterResponse,
        CharactersInfoResponse,
        NewCharacterResponse,
        CharacterSelectResponse,

        // From GameServer
        VerifyTokenResponse,
        InitLevel,
        InitMainPlayer,
        SpawnEntity,
        EntityMove,
    }
    public enum ToGameServer : ushort
    {
        // From Client
        VerifyTokenRequest = 2000,
        PlayerMove,

        // From Authenticate
        GameServerInfoResponse,
        PlayerToken,
    }
    public enum ToGateway : ushort
    {
        // From CLient
        LoginRequest = 3000,
        RegisterRequest,
        NewCharacterRequest,
        CharacterSelectRequest,

        // From Authenticate
        LoginAuthResponse,
        RegisterAuthResponse,
        CharactersInfoAuthResponse,
        NewCharacterAuthResponse,
        CharacterSelectAuthResponse,
    }
    public enum ToAuthenticate : ushort
    {
        // From Gateway
        LoginAuthRequest = 4000,
        RegisterAuthRequest,
        NewCharacterAuthRequest,
        CharacterSelectAuthRequest,

        // From GameServer
        GameServerInfoRequest,
    }
}