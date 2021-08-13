using mirobo_console.MiRobo;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace mirobo_console
{
    public class MiRoboVacuum : IDisposable
    {
        //todo: provide params from console

        private readonly IOutputProvider outputProvider = new ConsoleOutputProvider();

        public Process cmdProcess { get; }

        public MiRoboVacuum()
        {
            var cmdPath = @"C:\Windows\System32\cmd.exe";
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = cmdPath;
            cmdStartInfo.RedirectStandardOutput = true;
            cmdStartInfo.RedirectStandardError = true;
            cmdStartInfo.RedirectStandardInput = true;
            cmdStartInfo.UseShellExecute = false;
            cmdStartInfo.CreateNoWindow = true;

            cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.ErrorDataReceived += cmd_Error;
            cmdProcess.OutputDataReceived += cmd_DataReceived;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();
        }

        public void EnqueueCommand(IMiRoboCommand command) => ExecuteCommand(miroboPath, "--ip", ip, "--token", token, command.GenerateAction());
        

        private void ExecuteCommand(params string[] commads) => cmdProcess.StandardInput.WriteLine(string.Join(" ", commads));

        private void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            //outputProvider.WriteLine(e.Data);
        }

        private void cmd_Error(object sender, DataReceivedEventArgs e)
        {
            //outputProvider.WriteLine(e.Data);
        }

        public void Dispose()
        {
            ExecuteCommand("exit");

            cmdProcess.WaitForExit();
        }
    }
}