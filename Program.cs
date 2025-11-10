using System;
using System.IO;
using System.IO.Pipes;
using Serilog;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;


public class Master
{


    static Master()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    private static JObject path = JObject.Parse(
        File.ReadAllText("assets/json/paths.json")
    );
    private static string accessPath(string key)
    {
        return path[key]!.ToString();
    }

    private static void show(string any)
    {
        Console.WriteLine(any);
    }

    private static string input(string text, bool enter = false)
    {
        Console.Write(enter ? text + Environment.NewLine : text);
        return Console.ReadLine()!;
    }

    public static void generateNewProfile(string username)
    {
        JObject profile = new JObject
        {
            {"username", username},
            {"vault 💎", "100"},
            {"currency", "Relich"},
            {"level", 1},
            {"xp", 0},
            {"day", 1},
            {"stats", new JObject{
                {"total_profit", 0},
                {"successful_deals", 0},
                {"failed_negotiations", 0},
                {"items_bought", 0},
                {"items_sold", 0},
            }},
        {"shop", new JObject
            {
                {"name", "Second Chance Store"},
                {"inventory_slots", 10},
                {"inventory_used", 10},
            }
        }};
        File.WriteAllText(
            $"{path["profiles"]!.ToString()}/{username}.json", profile.ToString(Newtonsoft.Json.Formatting.Indented)
        );
    }

    private static void typeEffect(JToken lines, int delay = 30)
    {
        foreach (var line in lines!)
        {
            foreach (char c in line.ToString())
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        Thread.Sleep(500);
        input("\n\nPress enter key to continue . . .");
    }

    private static void continues()
    {
        Console.Clear();

        string[] files = Directory.GetFiles(accessPath("profiles"));
        List<string> profiles = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            // Console.WriteLine($"{i}: {files[i]}");
            string template = File.ReadAllText(accessPath("continue_profile"));
            JObject profile = JObject.Parse(File.ReadAllText(files[i]));

            profiles.Add(
                template
                    .Replace("{index}", (i + 1).ToString())
                    .Replace("{username}", profile["username"]!.ToString())
                    .Replace("{level}", profile["level"]!.ToString())
                    .Replace("{day}", profile["day"]!.ToString())
                    .Replace("{vault}", profile["vault"]!.ToString())
                    .Replace("{item}", profile["shop"]!["inventory_used"]!.ToString())
            );
        }
        string template_continue = File.ReadAllText(accessPath("continue_select"));
        template_continue = template_continue.Replace(
            "{profiles}", string.Join("\n", profiles)
        );
        show(template_continue);
    }

    public static void newGame()
    {
        Console.Clear();

        show(File.ReadAllText(accessPath("newGame")));
        Console.Write("insert username: ");
        string username = Console.ReadLine()!;

        generateNewProfile(username);
        greetings(username);
    }

    public static void dashboard()
    {
        Console.WriteLine("ini dashboard");
    }

    private static void firstGame()
    {
        show(File.ReadAllText(accessPath("firstWelcome")));

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
        show(File.ReadAllText(accessPath("welcome")));

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
        JObject __greetings = JObject.Parse(
            File.ReadAllText(
                accessPath("greetings_dialog")
            )
        );

        show(File.ReadAllText(accessPath("greetings_banner")));
        typeEffect(
            __greetings["intro_opening"]!
        );

        Console.Clear();
        show(File.ReadAllText(accessPath("greetings_banner")));
        typeEffect(__greetings["intro_tutorial"]!);

        Console.Clear();
        show(File.ReadAllText(accessPath("in_game_store_banner")));
        show(File.ReadAllText(accessPath("in_game_dashboard")));
    }

    public static void Main(string[] args)
    {
        Console.Clear();

        string[] profiles = Directory.GetFiles(accessPath("profiles"));
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