using System.Collections.Generic;
using Test;

namespace WebApi
{
    public class Logger : ILogger
    {
        public List<string> Logs { get; } = new List<string>();
        private readonly object syncObj = new object();

        public void Log(string str)
        {
            lock (syncObj)
            {
                Logs.Add(str);
            }
        }
    }
}