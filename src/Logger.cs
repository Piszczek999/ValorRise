namespace MMOLibrary;
using Riptide.Utils;

public static class Logger
{
    public static void Debug(string message)
    {
        RiptideLogger.Log(LogType.Debug, message);
    }

    public static void Info(string message)
    {
        RiptideLogger.Log(LogType.Info, message);
    }

    public static void Warning(string message)
    {
        RiptideLogger.Log(LogType.Warning, message);
    }

    public static void Error(string message, Exception ex = null)
    {
        string errorMessage = ex == null ? message : $"{message}: {ex.Message}";
        RiptideLogger.Log(LogType.Error, errorMessage);
    }
}
