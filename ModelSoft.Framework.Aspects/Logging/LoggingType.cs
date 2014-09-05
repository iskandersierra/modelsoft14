using System;

namespace ModelSoft.Framework.Logging
{
    [Serializable]
    public enum LoggingType
    {
        EnterAndExit,
        Enter,
        UnhandledException
    }
}