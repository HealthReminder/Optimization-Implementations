using Colosso.Tools.Patterns;
using System;
using UnityEngine;

[Flags]
public enum LogChannel
{
    NONE            = 0,
    LOW             = 1 << 0, // 1
    COLOSSO         = 1 << 1, // 2
    MID             = 1 << 2, // 4
    MATCH           = 1 << 3, // 8
    UI              = 1 << 4, // 16
    TUTORIAL        = 1 << 5, // 32
    INPUT           = 1 << 6, // 64
    UNKNOWN         = 1 << 10  
}
public class GlobalLogger : Singleton<GlobalLogger>
{
    [SerializeField] private LogChannel _activeChannels;
    [SerializeField] private bool _isLoggingWarnings = true;
    [SerializeField] private bool _isLoggingErrors = true;
    public void Log(LogChannel channel, string message)
    {
        if (_activeChannels.HasFlag(channel))
            Debug.Log(message);
    }
    public void Warning(string message)
    {
        if (!_isLoggingWarnings) return;
        Debug.LogWarning(message);

    }
    public void Error(string message)
    {
        if (!_isLoggingErrors) return;
        Debug.LogError(message);

    }
}
