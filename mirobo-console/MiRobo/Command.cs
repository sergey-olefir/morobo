using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mirobo_console.MiRobo
{
    public interface IMiRoboCommand
    {
        string GenerateAction();
    }

    public struct MoveForwardCommand : IMiRoboCommand
    {
        private int speed;

        public MoveForwardCommand(int speed)
        {
            this.speed = speed;
        }

        public string GenerateAction() => $"manual forward {(speed > ushort.MaxValue / 2 ? "0.2" : "0.1")}";
    }

    public struct MoveBackwardCommand : IMiRoboCommand
    {
        private int speed;

        public MoveBackwardCommand(int speed)
        {
            this.speed = speed;
        }

        public string GenerateAction() => $"manual backward {(speed > ushort.MaxValue / 2 ? "0.2" : "0.1")}";
    }

    public struct MoveLeftCommand : IMiRoboCommand
    {
        private int degrees;

        public MoveLeftCommand(int degrees)
        {
            this.degrees = degrees;
        }

        public string GenerateAction() => $"manual left {degrees}";        
    }

    public struct MoveRightCommand : IMiRoboCommand
    {
        private int degrees;

        public MoveRightCommand(int degrees)
        {
            this.degrees = degrees;
        }

        public string GenerateAction() => $"manual right {degrees}";
    }

    public struct ManualStartCommand : IMiRoboCommand
    {
        public string GenerateAction() => "manual start";
    }

    public struct ManualStopCommand : IMiRoboCommand
    {
        public string GenerateAction() => "manual stop";
    }

    public struct EmptyCommand : IMiRoboCommand
    {
        public string GenerateAction() => throw new InvalidOperationException();
    }
}
