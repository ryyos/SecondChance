using System.Text.Json.Nodes;
using Newtonsoft.Json;

public class Funct
{
    private static Paths paths;
    private static Greetings greetings;
    private static Profile profile;

    static Funct()
    {
        paths = JsonConvert.DeserializeObject<Paths>(
            File.ReadAllText("assets/json/paths.json")
        )!;
        greetings = JsonConvert.DeserializeObject<Greetings>(
            File.ReadAllText(paths.greetings_dialog)
        )!;

        profile = JsonConvert.DeserializeObject<Profile>(
            File.ReadAllText("data/profiles/llach.json")
        )!;
    }
    public static void generateNewProfile(string username)
    {

        Profile profile = new Profile
        {
            username = username,
            vault = 100,
            currency = "Relich",
            level = 1,
            xp = 0,
            day = 1,
            stats = new Stats
            {
                total_profit = 0,
                successful_deals = 0,
                failed_negotiations = 0,
                items_bought = 0,
                items_sold = 0
            },
            shop = new Shop
            {
                name = "Second Chance Store",
                inventory_slots = 10,
                inventory_used = 0
            }
        };
        File.WriteAllText(
            paths.profiles, JsonConvert.SerializeObject(profile, Formatting.Indented)
        );
    }

    private static string input(string text, bool enter = false)
    {
        Console.Write(enter ? text + "\n" : text);
        return Console.ReadLine()!;
    }

    private static void show(string any)
    {
        Console.WriteLine(any);
    }

    public static string randomDialogs(string key)
    {
        Console.WriteLine(profile.stats.total_profit);
        return "";
    }
    private static void typeEffect(string[] lines, int delay = 30)
    {
        foreach (var line in lines)
        {
            foreach (char c in line)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        Thread.Sleep(500);
        input("\n\nPress enter key to continue . . .");
    }


    public static void showGreetings(string username)
    {
        Console.Clear();

        show(File.ReadAllText(paths.greetings_banner));
        typeEffect(greetings.intro_opening);

        Console.Clear();
        show(File.ReadAllText(paths.greetings_banner));
        typeEffect(greetings.intro_tutorial);

        Console.Clear();
        show(File.ReadAllText(paths.in_game_store_banner));
        show(File.ReadAllText(paths.in_game_dashboard));
    }

    public static void convertToExcel()
    {
    }
}