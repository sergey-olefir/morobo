using mirobo_console.MiRobo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mirobo_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var gamepad = new Gamepad();

            using (var cleaner = new MiRoboVacuum())
            {
                var processor = new CommandProcessor1();
                using (gamepad.ProduceEvents().Subscribe(a =>
                {
                    var command = processor.ProvideCommand(a);

                    if (command != null)
                    {
                        Console.WriteLine(command.GenerateAction());
                        cleaner.EnqueueCommand(command);
                    }
                }, () => Console.WriteLine("end")))
                {
                    Console.ReadLine();
                }
            }

            Console.ReadLine();
        }
    }
}