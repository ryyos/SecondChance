using System;
using System.IO;
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

    public static void Continue()
    {

    }

    public static void NewGame()
    {

    }

    public static void Dashboard()
    {

    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Halo ini Julian");
        Console.WriteLine(
            File.ReadAllText(pathWelcome)
        );

        Console.Write("Select an option: ");
        bool status = int.TryParse(Console.ReadLine()!, out int chose);
        if (!status)
        {
            Log.Error("input not valid");
        }
        ;
    }
}