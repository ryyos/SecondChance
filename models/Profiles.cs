
public class Profile
{
    public string username { get; set; }
    public int vault { get; set; }
    public string currency { get; set; }
    public int level { get; set; }
    public int xp { get; set; }
    public int day { get; set; }
    public Stats stats { get; set; }
    public Shop shop { get; set; }
    public Inventory inventory { get; set; } 
    public Logs logs { get; set; } 
}

public class Shop
{
    public string name { get; set; }
    public int inventory_slots { get; set; }
    public int inventory_used { get; set; }
}

public class Stats
{
    public int total_profit { get; set; }
    public int successful_deals { get; set; }
    public int failed_negotiations { get; set; }
    public int items_bought { get; set; }
    public int items_sold { get; set; }
}

public class Inventory
{
    public string name { get; set; }
    public int price { get; set; }
    public string category { get; set; }
    public string time { get; set; }
}

public class Logs
{
    public string name { get; set; }
    public string time { get; set; }
    public int relich { get; set; }
    public int status { get; set; }
}