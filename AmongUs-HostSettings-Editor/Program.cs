using System;
using System.IO;
using System.Threading;

namespace AmongUs_HostSettings_Editor
{
    class Program
    {
        private static readonly string hostSettingsPath = $@"C:\Users\{Environment.UserName}\AppData\LocalLow\Innersloth\Among Us\gameHostOptions";

        private static void EditHostSettings(int pos)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nType a decimal value for the selected setting, it will automatically be converted to hexadecimal.");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\r\nNew value: ");
            if (!int.TryParse(Console.ReadLine(), out int dec))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nIllegal input :(");
                Thread.Sleep(1000);
                return;
            }
            if (dec > 255 || dec < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPlease type a value between 0 and 255 :(");
                Thread.Sleep(1000);
                return;
            }
            try
            {
                using (var stream = new FileStream(hostSettingsPath, FileMode.Open, FileAccess.ReadWrite))
                {
                    stream.Position = pos;
                    stream.WriteByte((byte)dec);
                    stream.Close();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nSetting succesfully edited :)");
                Thread.Sleep(1000);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nSomething went wrong :(");
                Thread.Sleep(1000);
            }
        }

        private static void OptionsMenu()
        {
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Among Us host settings editor made with love by ElLondiuh");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nWARNING! Changing some settings with custom values may cause game instability");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1) Common tasks\n" +         //23
                              "2) Short tasks\n" +          //24
                              "3) Long tasks\n" +           //25
                              "4) Selected map\n" +         //6
                              "5) Impostors\n" +            //30
                              "6) Player speed\n" +         //9
                              "7) Kill distance\n" +        //31
                              "8) Kill cooldown\n" +        //21
                              "9) Emergency meetings");     //26

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\r\nSelect an option: ");
            switch (Console.ReadLine())
            {
                case "1":
                    EditHostSettings(23);
                    return;
                case "2":
                    EditHostSettings(25);
                    return;
                case "3":
                    EditHostSettings(24);
                    return;
                case "4":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n0=The Skeld | 1=MIRA HQ | 2=Polus");
                    EditHostSettings(6);
                    return;
                case "5":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nIf the value is larger than 3 the game with change it back to 3");
                    EditHostSettings(30);
                    return;
                case "6":
                    EditHostSettings(9);
                    return;
                case "7":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n0=Short | 1=Medium | 2=Long");
                    EditHostSettings(31);
                    return;
                case "8":
                    EditHostSettings(21);
                    return;
                case "9":
                    EditHostSettings(26);
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid option :(");
                    Thread.Sleep(500);
                    return;

            }
        }

        static void Main(string[] args)
        {
            if (!File.Exists(hostSettingsPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Among Us data not found");
                Thread.Sleep(1500);
                Environment.Exit(2);
            }
            while (true)
            {
                OptionsMenu();
            }
        }
    }
}
