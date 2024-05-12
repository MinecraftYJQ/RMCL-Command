using KMCCC.Authentication;
using KMCCC.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMCL_Command.cs
{
    internal class Launch_Main
    {
        public static LauncherCore Core = LauncherCore.Create();
        KMCCC.Launcher.Version[] versions = Core.GetVersions().ToArray();
        public Launch_Main()
        {
            string[] subDirectories = null;
            int sz = 0;
            try
            {
                string folderPath = ".minecraft\\versions";
                subDirectories = Directory.GetDirectories(folderPath);
                int i = 0;
                foreach (string subDir in subDirectories)
                {
                    i++;
                    string folderName = new DirectoryInfo(subDir).Name;
                    //Console.WriteLine($"当前:{folderName}");
                    if (folderName == File.ReadAllText("RMCL\\Game_Version.txt"))
                    {
                        sz = i-1;
                        break;
                    }
                }
            }
            catch{ }
            WindowSize Game_Size = null;
            Game_Size = new WindowSize { Height = 520, Width = 870 };
            try
            {
                Core.JavaPath = File.ReadAllText("RMCL\\Java_Path.txt");
                var ver = versions[sz];
                var result = Core.Launch(new LaunchOptions
                {
                    Version = ver,
                    MaxMemory = 1024,
                    Authenticator = new OfflineAuthenticator(File.ReadAllText("RMCL\\Player_Name.txt")),
                    Mode = LaunchMode.MCLauncher,
                    Size = Game_Size
                });
            }
            catch (Exception ex){ }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[ O K ]:");
            Console.ResetColor();
            Console.WriteLine("The command was executed successfully!");
        }
    }
}
