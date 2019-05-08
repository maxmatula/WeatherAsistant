using System.Collections.Generic;

namespace WAP.Infrastructure.Commands
{
    public static class CommandsList
    {
        public static List<string> commands = new List<string>()
        {
            "'exit' --> exit the application",
            "'weather city' or 'wc' --> Get Weather for specified CITY",
            "'show queries' or 'sq' --> Log last 100 user queries to console",
            "'show queries analisys' or 'sqa' --> Show prediction algorithm data - (works only after 3 queries)"
        };
    }
}
