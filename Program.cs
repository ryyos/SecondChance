using System;
using System.IO;
using System.IO.Pipes;
using Serilog;

public class Master
{

    private static string pathWelcome = "assets/txt/welcome.txt";
    private static string pathLogs = "logs/log_.txt";
    static Master()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(pathLogs, rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    public static void continues()
    {
        Console.WriteLine("ini continue");
    }

    public static void newGame()
    {
        Console.WriteLine("ini newGame");
    }

    public static void dashboard()
    {
        Console.WriteLine("ini dashboard");
    }

    private static void show(string any)
    {
        Console.WriteLine(any);
    }

    public static void Main(string[] args)
    {
        show(File.ReadAllText(pathWelcome));

        Console.Write("Select an option: ");
        bool status = int.TryParse(Console.ReadLine()!, out int chose);
        switch (chose)
        {
            case 1:
                continues();
                break;

            case 2:
                newGame();
                break;

            case 3:
                dashboard();
                break;

            default:
                Log.Warning("option not valid");
                break;
        }


        if (!status)
        {
            Log.Error("input not valid");
        }
    }
}