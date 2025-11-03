using System;
using System.IO;
using System.IO.Pipes;
using Serilog;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

public class Master
{

    private static string pathWelcome = "assets/txt/welcome.txt";
    private static string pathFirstWelcome = "assets/txt/welcome_first.txt";
    private static string pathLogs = "logs/log_.txt";
    private static string pathNewGame = "assets/txt/newGame.txt";
    private static string pathProfiles = "data/profiles";
    private static string pathGreetings = "assets/txt/greetings.txt";
    private static string pathContinue = "assets/txt/continue.txt";

    static Master()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(pathLogs, rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    private static void generateNewProfile(string username)
    {
        string data = string.Format(@"
            {{
                'username': '{0}',
                'balance💎': 100,
                'level': 1
            }}", username);

        JObject jdata = JObject.Parse(data);
        File.WriteAllText($"{pathProfiles}/{username}.json", jdata.ToString(Newtonsoft.Json.Formatting.Indented));
    }

    public static void continues()
    {
        Console.Clear();
        show(File.ReadAllText(pathContinue));
    }

    public static void newGame()
    {
        Console.Clear();

        show(File.ReadAllText(pathNewGame));
        Console.Write("insert username: ");
        string username = Console.ReadLine()!;
        generateNewProfile(username);
        greetings(username);
    }

    public static void dashboard()
    {
        Console.WriteLine("ini dashboard");
    }

    private static void show(string any)
    {
        Console.WriteLine(any);
    }

    private static void firstGame()
    {
        show(File.ReadAllText(pathFirstWelcome));

        Console.Write("Select an option: ");
        bool status = int.TryParse(Console.ReadLine()!, out int chose);
        switch (chose)
        {
            case 1:
                newGame();
                break;

            case 2:
                dashboard();
                break;

            case 0:
                Log.Information("thanks for playings!");
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

    private static void continueGame()
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

            case 0:
                Log.Information("thanks for playings!");
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

    private static void greetings(string username)
    {
        Console.Clear();
        JObject metadata = JObject.Parse(
            File.ReadAllText($"{pathProfiles}/{username}.json")
        );

        string info = string.Format(@"
username: {0}
level: {1}
balance💎: {2}
        ", username, metadata["level"]!.Value<int>(), metadata["balance💎"]!.Value<int>());

        show(File.ReadAllText(pathGreetings));
        show(info);
    }

    public static void Main(string[] args)
    {
        string[] profiles = Directory.GetFiles(pathProfiles);
        if (profiles.Length > 0)
        {
            continueGame();
        }
        else
        {
            firstGame();
        }
    }
}