namespace ValorRiseClient;

public static class MessageType
{
  public enum ToClient : ushort
  {
    // From Gateway
    LoginResponse,
    RegisterResponse,
    CharactersInfoResponse,
    NewCharacterResponse,
    CharacterSelectResponse,

    // From GameServer
    VerifyTokenResponse,
    InitLevel,
    InitMainPlayer,
    SpawnEntity,
    MoveEntity,
  }
  public enum ToGameServer : ushort
  {
    // From Client
    VerifyTokenRequest,

    // From Authenticate
    PlayerToken,
  }
  public enum ToGateway : ushort
  {
    // From CLient
    LoginRequest,
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
    LoginAuthRequest,
    RegisterAuthRequest,
    NewCharacterAuthRequest,
    CharacterSelectAuthRequest,

    // From GameServer

  }
}