using System;
using System.Collections.Generic;

namespace WebApi
{
    public static class Logger
    {
        public static List<string> Logs { get; } = new List<string>();

        public static void Log(string str)
        {
            Logs.Add(str);
        }
    }
}