﻿
namespace StreamWorld.LogManager
{
    public interface ILogger
    {
        void Log(string message, params object[] additionalData);
    }
    public interface ICustomLogger
    {
        void Log(string message, params object[] additionalData);
    }


}
