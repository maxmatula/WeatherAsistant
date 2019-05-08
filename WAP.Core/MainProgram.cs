using System;

namespace WAP.Core
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            var shell = new CoreShell();
            shell.ConsoleSetup();
            shell.Run();

            shell.ConsoleService();
        }
    }
}
