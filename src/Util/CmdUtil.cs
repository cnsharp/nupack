using System;
using System.Diagnostics;
using System.Threading;
using CnSharp.VisualStudio.Extensions;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public class CmdUtil
    {
        //cmd issues goes here:https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why
        public static void RunCmd(string script)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                //process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                using (var outputWaitHandle = new AutoResetEvent(false))
                using (var errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) => {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            Host.Instance.Dte2.OutputMessage(Common.ProductName, Environment.NewLine+e.Data);
                        }
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            Host.Instance.Dte2.OutputMessage(Common.ProductName, Environment.NewLine + e.Data);
                        }
                    };

                    process.Start();
                    using (var writer = process.StandardInput)
                    {
                        if (writer.BaseStream.CanWrite)
                        {
                            writer.WriteLine(script);
                        }
                    }

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }
            }
        }
    }
}
