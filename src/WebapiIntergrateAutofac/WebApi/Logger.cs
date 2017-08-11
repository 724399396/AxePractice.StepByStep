using System.Collections.Generic;

namespace WebApi
{
    public class Logger : ILogger
    {
        public List<string> Logs { get; } = new List<string>();
        readonly object syncObj = new object();

        public void Log(string str)
        {
            lock (syncObj)
            {
                Logs.Add(str);
            }
        }
    }
}