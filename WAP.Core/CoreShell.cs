using WAP.Infrastructure.Commands;
using System;

namespace WAP.Core
{
    public class CoreShell
    {
        public void Run()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hi");
            Console.WriteLine("Please type command!");
            Console.Write("If you don't know any commands just type ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("--help");
            Console.ResetColor();
            Console.Write(" for list of availiable commands.");
            Console.WriteLine("");
        }

        public void ConsoleSetup()
        {
            Console.Title = "W.A.P";
        }

        public void ConsoleService()
        {
            string command = "";
            var commandService = new CommandsService();

            //Console Interface
            while (true)
            {
                Console.WriteLine();
                Console.Write("W.A.P -->: ");
                command = Console.ReadLine();
                commandService.CheckCommand(command);
            };
        }
    }
}
