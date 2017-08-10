using System.Collections.Generic;

namespace WebApi
{
    public static class Logger
    {
        public static List<string> Logs { get; } = new List<string>();
        private static readonly object syncObj = new object();

        public static void Log(string str)
        {
            lock (syncObj)
            {
                Logs.Add(str);
            }
        }
    }
}