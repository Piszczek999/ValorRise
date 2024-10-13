namespace ValorRise.Enums;

public enum PacketType
{
    #region ToClient

    // From Gateway
    LoginResponse = 1000,
    RegisterResponse,
    CharacterInfosResponse,
    NewCharacterResponse,
    CharacterSelectResponse,

    // From GameServer
    VerifyTokenResponse,
    MapInfo,
    PlayerInfo,
    SpawnEntity,
    EntityMove,

    #endregion

    #region ToGameserver

    // From Client
    PlayerAuthenticate = 2000,
    PlayerMovement,

    // From Authenticate
    GameServerInfoResponse,
    CharacterToken,

    #endregion

    #region ToGateway

    // From CLient
    LoginRequest = 3000,
    RegisterRequest,
    NewCharacterRequest,
    CharacterSelectRequest,

    // From Authenticate
    LoginAuthResponse,
    RegisterAuthResponse,
    CharacterInfosAuthResponse,
    NewCharacterAuthResponse,
    CharacterSelectAuthResponse,

    #endregion

    #region ToAuthenticate

    // From Gateway
    LoginAuthRequest = 4000,
    RegisterAuthRequest,
    NewCharacterAuthRequest,
    CharacterSelectAuthRequest,

    // From GameServer
    GameServerInfoRequest,

    #endregion
}