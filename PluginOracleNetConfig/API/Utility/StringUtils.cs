using System;
using System.Text;

namespace PluginOracleNetConfig.API.Utility
{
    public static class StringUtils
    {
        public static string ToAllCaps(this string s)
        {
            var allCapsBuilder = new StringBuilder();
            
            s.ForEach(c => allCapsBuilder.Append(c.ToString().ToUpper()));

            return allCapsBuilder.ToString();
        }

        public static void ForEach(this string s, Action<char> loopAction)
        {
            foreach (var c in s)
            {
                loopAction(c);
            }
        }
    }
}