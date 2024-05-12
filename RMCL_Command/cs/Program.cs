using System;
using System.Drawing;
using System.Runtime.ExceptionServices;

namespace RMCL_Command.cs
{
    internal class Program
    {
        static string command(string str)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine(str + "&exit");
                string output = p.StandardOutput.ReadToEnd();
                p.StandardInput.AutoFlush = true;
                return output;
            }
            catch (Exception ex)
            {
                //Error(2, ex.ToString());
                return null;
            }
        }
        static void Main(string[] args)
        {
            Reset();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("RMCL@Terminal-Command:");
                Console.ResetColor();
                Console.Write("~$ ");
                string cmd_in = Console.ReadLine();
                if (cmd_in.Length > 0)
                {
                    if (cmd_in == "help")
                    {
                        help();
                    }
                    else if (cmd_in == "cls")
                    {
                        Reset();
                    }else if(cmd_in== "resetGUI")
                    {
                        try
                        {
                            File.WriteAllText("RMCL\\GUI.txt", "true");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("[ O K ]:");
                            Console.ResetColor();
                            Console.WriteLine("The command was executed successfully!");
                        }
                        catch(Exception Ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[Error]:");
                            Console.ResetColor();
                            Console.WriteLine(Ex.Message);
                        }
                    }else if (cmd_in == "gamelist")
                    {
                        string[] subDirectories = null;
                        string folderPath = ".minecraft\\versions";
                        subDirectories = Directory.GetDirectories(folderPath);
                        int i = 0;
                        foreach (string subDir in subDirectories)
                        {
                            string folderName = new DirectoryInfo(subDir).Name;
                            Console.WriteLine($"游戏版本:{folderName}");
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[ O K ]:");
                        Console.ResetColor();
                        Console.WriteLine("The command was executed successfully!");
                    }
                    else if (cmd_in == "launch")
                    {
                        Launch_Main launch_Main = new Launch_Main();
                    }else if (cmd_in == "javalist")
                    {
                        string[] lines = command("where javaw").Substring(command("where javaw").IndexOf("&exit") + 6).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        for(int i=1;i<=lines.LongLength-2; i++)
                        {
                            Console.WriteLine(lines[i]);
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[ O K ]:");
                        Console.ResetColor();
                        Console.WriteLine("The command was executed successfully!");
                    }
                    else if (cmd_in.Length > 6)
                    {
                        try
                        {
                            if (cmd_in.Substring(0, 7) == "setting")
                            {
                                if (cmd_in.Length == 7)
                                {
                                    Console.WriteLine("setting 设置参数程序使用:");
                                    Console.WriteLine("setting version:<版本>                  设置默认版本");
                                    Console.WriteLine("setting username:<玩家名>               设置默认玩家名");
                                    Console.WriteLine("setting javapath:<Java路径>             设置默认Java路径");
                                }
                                //Console.WriteLine(cmd_in.Substring(8, 7));
                                else if (cmd_in.Substring(8, 10) == "playername")
                                {
                                    //Console.WriteLine(cmd_in.Substring(19, cmd_in.Length - 19));
                                    File.WriteAllText("RMCL\\Player_Name.txt", cmd_in.Substring(19, cmd_in.Length - 19));
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("[ O K ]:");
                                    Console.ResetColor();
                                    Console.WriteLine("The command was executed successfully!");
                                }
                                else if(cmd_in.Substring(8, 7) == "version")
                                {
                                    //Console.WriteLine(cmd_in.Substring(16,cmd_in.Length - 16));
                                    File.WriteAllText("RMCL\\Game_Version.txt", cmd_in.Substring(16, cmd_in.Length - 16));
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("[ O K ]:");
                                    Console.ResetColor();
                                    Console.WriteLine("The command was executed successfully!");
                                }
                                else if(cmd_in.Substring(8, 8) == "javapath")
                                {
                                    File.WriteAllText("RMCL\\Java_Path.txt",cmd_in.Substring(17,cmd_in.Length - 17));
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("[ O K ]:");
                                    Console.ResetColor();
                                    Console.WriteLine("The command was executed successfully!");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("[Error]:");
                                    Console.ResetColor();
                                    Console.WriteLine($"RMCL Not found \"{cmd_in}\" Command!");
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("[Error]:");
                                Console.ResetColor();
                                Console.WriteLine($"RMCL Not found \"{cmd_in}\" Command!");
                            }
                        }
                        catch (Exception Ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[Error]:");
                            Console.ResetColor();
                            Console.WriteLine($"RMCL Not found \"{cmd_in}\" Command!");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[Error]:");
                        Console.ResetColor();
                        Console.WriteLine($"RMCL Not found \"{cmd_in}\" Command!");
                    }
                }
            }
        }
        static void Reset()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("RMCL - Terminal");
            Console.ResetColor();
            Console.WriteLine("  ____  __  __  ____ _     ");
            Console.WriteLine(" |  _ \\|  \\/  |/ ___| |    ");
            Console.WriteLine(" | |_) | |\\/| | |   | |    ");
            Console.WriteLine(" |  _ <| |  | | |___| |___        Terminal");
            Console.WriteLine(" |_| \\_\\_|  |_|\\____|_____|       By:MinecraftYJQ");
            Console.WriteLine("                           ");
            Console.ResetColor();
            Console.WriteLine("键入help获取帮助");
            try
            {
                if (File.ReadAllText("RMCL\\GUI.txt") == "true")
                {
                    System.Environment.Exit(0);
                }
            }
            catch
            {
                System.Environment.Exit(0);
            }
            Console.Title = "RMCL - Command";

        }
        static void help()
        {
            Console.WriteLine("帮助程序:");
            Console.WriteLine("help      帮助程序");
            Console.WriteLine("cls       清除屏幕内容");
            Console.WriteLine("resetGUI  返回RMCL的GUI启动器");
            Console.WriteLine("gamelist  输出所有游戏版本");
            Console.WriteLine("javalist  输出所有Java路径");
            Console.WriteLine("launch    以默认设置启动Minecraft");
            Console.WriteLine("setting   设置默认参数");
        }
    }
}