namespace ValorRise.Enums;

public enum PacketType : ushort
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
    InitInfo,
    EntitySpawn,
    ProjectileSpawn,
    DropItemSpawn,
    ObjectDespawn,
    WorldPositionState,
    EntityStatsState,
    WorldHealthState,
    EntityHealthChange,
    EntitySpellCast,
    ProjectileHitEntity,
    EntityAttack,
    SlotUpdate,

    #endregion

    #region ToGameserver

    // From Client
    Authenticate = 2000,
    ClientMove,
    ClientAttack,
    ClientSlotUse,
    ClientSlotsSwap,

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
    UserLogout,
    CharacterLogout,
    CharacterSwitchServer,

    #endregion
}