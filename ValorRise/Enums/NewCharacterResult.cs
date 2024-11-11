namespace ValorRise.Enums;

public enum NewCharacterResult : byte
{
    Success,
    CharacterLimitReached,
    NameTaken,
    FailedToCreateCharacter
}