using SharpDX.DirectInput;
using System;

namespace mirobo_console.MiRobo
{
    public class CommandProcessor
    {
        public IMiRoboCommand ProvideCommand(JoystickUpdate update)
        {
            switch (update.Offset)
            {
                case JoystickOffset.Buttons9:
                    return new ManualStartCommand();
                case JoystickOffset.Buttons8:
                    return new ManualStopCommand();
                case JoystickOffset.RotationY:
                    return new MoveBackwardCommand(update.Value);
                case JoystickOffset.RotationX:
                    return new MoveForwardCommand(update.Value);
                case JoystickOffset.X when Math.Abs(update.Value - ushort.MaxValue / 2) > 20000:
                    if (update.Value > ushort.MaxValue / 2)
                        return new MoveRightCommand(30);
                    else
                        return new MoveLeftCommand(30);
                default:
                    return null;
            };
        }
    }

    public class CommandProcessor1
    {
        public IMiRoboCommand ProvideCommand(JoystickState state)
        {
            if (state.Buttons[9])
            {
                return new ManualStartCommand();
            }
            if (state.Buttons[8])
            {
                return new ManualStopCommand();
            }
            if (state.RotationY != 0)
            {
                return new MoveForwardCommand(state.RotationY);
            }
            if (state.RotationX != 0)
            {
                return new MoveBackwardCommand(state.RotationX);
            }
            var xOffset = Math.Abs(state.X - ushort.MaxValue / 2);
            var lambda = 5000;
            var value = (ushort.MaxValue / 2 - lambda) / 45;
            if (xOffset > lambda)
            {
                var degrees = (xOffset - lambda) / value;
                if (state.X > ushort.MaxValue / 2) //get direction
                    return new MoveRightCommand(degrees);
                else
                    return new MoveLeftCommand(degrees);
            }

            return null;
        }
    }
}