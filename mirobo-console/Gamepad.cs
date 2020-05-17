using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mirobo_console
{
    public class Gamepad
    {
        private Joystick joystick;

        public Gamepad()
        {
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.FirstPerson,
                        DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            // Instantiate the joystick
            joystick = new Joystick(directInput, joystickGuid);

            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);
        }

        public IObservable<JoystickState> ProduceEvents()
        {
            // Set BufferSize in order to use buffered data.
            joystick.Properties.BufferSize = 256;

            // Acquire the joystick
            joystick.Acquire();

            // Poll events from joystick
            return Observable.Create<JoystickState>((o, ct) =>
            {
                return Task.Run(() =>
                {
                    while (!ct.IsCancellationRequested)
                    {
                        o.OnNext(joystick.GetCurrentState());
                        Thread.Sleep(1000);
                    }
                    o.OnCompleted();
                }, ct);
            });
        }
    }
}