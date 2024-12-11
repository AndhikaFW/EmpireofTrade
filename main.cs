//Adrian Dika Darmawan 2306250711
//Andhika Fadhlan Wijanarko 2306267164
//Jonathan Matius 2306161896

//Empire of Trade
using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }

    public Product(string name, int buyPrice, int sellPrice)
    {
        Name = name;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
    }
}

class Merchant
{
    public string Name { get; set; }
    public int Gold { get; set; }
    public int Xp { get; set; }
    public int Rep { get; set; }
    public int Buys { get; set; }
    public int Sells { get; set; }
    public Dictionary<string, int> Inventory { get; set; }

    public Merchant(string name, int gold, int xp, int rep, int buys, int sells)
    {
        Name = name;
        Gold = gold;
        Xp = xp;
        Rep = rep;
        Buys = buys;
        Sells = sells;

        Inventory = new Dictionary<string, int>();
    }

    public void Buy(Product product, int quantity)
    {
        int cost = product.BuyPrice * quantity;
        if (Gold >= cost)
        {
            Gold -= cost;
            if (Inventory.ContainsKey(product.Name))
                Inventory[product.Name] += quantity;
            else
                Inventory[product.Name] = quantity;

            product.BuyPrice += quantity;
            Console.WriteLine($"{Name} bought {quantity} {product.Name}(s) for {cost} gold.");
            Buys++;
            Xp++;
            Rep+=quantity;
        }
        else
        {
            Console.WriteLine("Not enough gold!");
        }
    }

    public void Sell(Product product, int quantity)
    {
        if (Inventory.ContainsKey(product.Name) && Inventory[product.Name] >= quantity)
        {
            int revenue = product.SellPrice * quantity;
            Gold += revenue;
            Inventory[product.Name] -= quantity;

            product.SellPrice -= quantity;
            Console.WriteLine($"{Name} sold {quantity} {product.Name}(s) for {revenue} gold.");
            Sells++;
            Xp++;
            Rep+=quantity;
        }
        else
        {
            Console.WriteLine("Not enough inventory!");
        }
    }

    public void ShowInventory()
    {
        Console.WriteLine($"\n{Name}'s Inventory:");
        foreach (var item in Inventory)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
        Console.WriteLine($"Gold: {Gold}\n");
        Console.WriteLine($"XP: {Xp}\n");
        Console.WriteLine($"Reputation: {Rep}\n");
        Console.WriteLine($"Buys: {Buys}\n");
        Console.WriteLine($"Sells: {Sells}\n");
    }
}

class Program
{
    static void Main()
    {
        Merchant player = new Merchant("Player", 100, 1, 0, 0, 0);
        List<Product> products = new List<Product>
        {
            new Product("Silk", 10, 15),
            new Product("Spice", 11, 16),
            new Product("Iron", 12, 17),
            new Product("Gem", 13, 18),
            new Product("DogeCoin", 14, 19),
            new Product("Solana", 15, 20),
            new Product("Ethereum", 16, 21),
            new Product("Bitcoin", 17, 22)
        };

        bool playing = true;
        while (playing)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Empire of Trade!");
            player.ShowInventory();

            Console.WriteLine("Available Products:");
            for (int i = 0; i < products.Count && i < player.Xp; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - Buy: {products[i].BuyPrice} Gold, Sell: {products[i].SellPrice} Gold");
            }

            Console.WriteLine("\nChoose an action: 1) Buy 2) Sell 3) Quit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the product number to buy: ");
                    if (int.TryParse(Console.ReadLine(), out int buyIndex) && buyIndex > 0 && buyIndex <= products.Count && buyIndex <= player.Xp)
                    {
                        Console.Write("Enter quantity: ");
                        if (int.TryParse(Console.ReadLine(), out int buyQuantity) && buyQuantity > 0)
                        {
                            player.Buy(products[buyIndex - 1], buyQuantity);
                        }
                    }
                    break;

                case "2":
                    Console.Write("Enter the product number to sell: ");
                    if (int.TryParse(Console.ReadLine(), out int sellIndex) && sellIndex > 0 && sellIndex <= products.Count)
                    {
                        Console.Write("Enter quantity: ");
                        if (int.TryParse(Console.ReadLine(), out int sellQuantity) && sellQuantity > 0)
                        {
                            player.Sell(products[sellIndex - 1], sellQuantity);
                        }
                    }
                    break;

                case "3":
                    playing = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        Console.WriteLine("Thanks for playing!");
    }
}
