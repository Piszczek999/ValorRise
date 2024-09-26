namespace MMOLibrary;

public static class MessageType
{
  public enum ToClient : ushort
  {
    LoginResult,
    RegisterResult,
    CharactersInfoResult,
    NewCharacterResult,
    CharacterSelectResult,
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
    CharactersInfoResponse,
    NewCharacterRequest,
    NewCharacterResponse,
    CharacterSelectRequest,
    CharacterSelectResponse
  }
  public enum ToAuthenticate : ushort
  {
    LoginAuthRequest,
    RegisterAuthRequest,
    NewCharacterAuthRequest,
    CharacterSelectAuthRequest
  }
}