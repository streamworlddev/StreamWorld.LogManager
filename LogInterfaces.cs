namespace Log
{
    public interface IColorableLogger
    {
        void Log(string message, System.Drawing.Color color);
    }

    public interface ILogger
    {
        void Log(string message);
    }
}
