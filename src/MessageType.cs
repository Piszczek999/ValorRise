namespace MMO_Library;

public static class MessageType
{
  public enum ToClient : ushort
  {
    LoginResponse,
    RegisterResponse,
    CharactersInfo,
    NewCharacterResponse,
    Character,
    TokenRequest,
    TokenVerificationResult,
  }
  public enum ToGameServer : ushort
  {
    Token,
    TokenResponse,
  }
  public enum ToGateway : ushort
  {
    LoginRequest,
    RegisterRequest,
    LoginResponse,
    RegisterResponse,
    CharactersInfo,
    NewCharacterRequest,
    NewCharacterResponse,
    SelectCharacterRequest,
    SelectCharacterResponse
  }
  public enum ToAuthenticate : ushort
  {
    LoginRequest,
    RegisterRequest,
    NewCharacterRequest,
    CharacterSelectRequest
  }
}