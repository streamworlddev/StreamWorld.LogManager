using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
