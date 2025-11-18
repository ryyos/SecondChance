using System;
using System.IO;
using System.IO.Pipes;
using Serilog;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;


public class Master
{

    private static void show(string any)
    {
        Console.WriteLine(any);
    }

    private static string input(string text, bool enter = false)
    {
        Console.Write(enter ? text + Environment.NewLine : text);
        return Console.ReadLine()!;
    }

    private static void continues()
    {
        Console.Clear();

        // string[] files = Directory.GetFiles(accessPath("profiles"));
        List<string> profiles = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            // Console.WriteLine($"{i}: {files[i]}");
            // string template = File.ReadAllText(accessPath("continue_profile"));
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
        // string template_continue = File.ReadAllText(accessPath("continue_select"));
        // template_continue = template_continue.Replace(
        //     "{profiles}", string.Join("\n", profiles)
        // );
        // show(template_continue);
    }

    public static void newGame()
    {
        Console.Clear();

        show(File.ReadAllText(accessPath("newGame")));
        Console.Write("insert username: ");
        string username = Console.ReadLine()!;

        Funct.generateNewProfile(username);
        Funct.showGreetings(username);
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