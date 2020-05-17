using System;

namespace mirobo_console
{
    public class ConsoleOutputProvider : IOutputProvider
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
