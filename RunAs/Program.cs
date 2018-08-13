using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Security;
using System.Net;

namespace RunAs
{
    class Program
    {
        static void Main(string[] args)
        {
            RunThisAsAdmin();
            Console.WriteLine("Running as admin!");
            Console.ReadKey();
        }

        private static void RunThisAsAdmin()
        {
            if (!IsAdministrator())
            {
                var exe = Process.GetCurrentProcess().MainModule.FileName;
                var startInfo = new ProcessStartInfo(exe)
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Normal,
                    CreateNoWindow = false
                };
                Process.Start(startInfo);
                Process.GetCurrentProcess().Kill();
            }
        }
        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
