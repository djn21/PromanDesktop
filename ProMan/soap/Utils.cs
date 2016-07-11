using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.soap
{
    public static class Utils
    {
        public static string parseNs(string value)
        {
            string[] parts = value.Split(':');
            return parts.Length > 1 ? parts[1] : parts[0];
        }
    }
}
