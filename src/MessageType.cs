namespace MMOLibrary;

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
    VerifyTokenResponse,
  }
  public enum ToGameServer : ushort
  {
    // From Client
    VerifyTokenRequest,

    // From Database
    VerifyTokenDBResponse,
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

    // From Database
    LoginDBResponse,
    RegisterDBResponse,
    CharactersInfoDBResponse,
    NewCharacterDBResponse,
    CharacterSelectDBResponse,
  }
  public enum ToDatabase : ushort
  {
    // From Authenticate
    LoginDBRequest,
    RegisterDBRequest,
    CharactersInfoDBRequest,
    NewCharacterDBRequest,
    CharacterSelectDBRequest,

    // From GameServer
    VerifyTokenDBRequest,
  }
}