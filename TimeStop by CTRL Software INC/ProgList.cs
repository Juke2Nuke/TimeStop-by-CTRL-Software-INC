using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace TimeStop_by_CTRL_Software_INC
{
    class ProgList
    {
        public readonly string Name;
        public readonly string Path;
        public readonly string Version;
        public bool Timer;
        public bool Warn;
        public bool Stop;
        public ProgList(string name, string path, string version, bool timer = true, bool warn = true, bool stop=true)
        {
            Name = name;
            Path = path;
            Version = version;
            Timer = timer;
            Warn = warn;
            Stop = stop;
        }
    }
}
