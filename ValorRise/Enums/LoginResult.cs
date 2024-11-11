namespace ValorRise.Enums;

public enum LoginResult : byte
{
    Success,
    IncorrectCredentials,
    FailedToLogin,
    AccountLockedOut,
    AlreadyLoggedIn
}