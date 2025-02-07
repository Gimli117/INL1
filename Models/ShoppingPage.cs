using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MenuTemplateForINL1.Models
{
    internal class ShoppingPage
    {
        public static List<Category> categories = new();
        public static void Run()
        {
            bool quit = false;
            {
                using (var db = new Models.MyDbContext())
                {
                    var shopItems = ItemStore.GetItems();
                    var shopCategories = db.INL1Categories.ToList();
                    while (!quit && !Program.exit)
                    {
                        bool loop = false;
                        int categoryKey;

                        Console.WriteLine("Welcome to the Shopping Page: Here are your keyboard shortcuts:\n");         // 3: Shopsida med minst tre kategorier

                        Console.WriteLine("1 - List all items");
                        Console.WriteLine("2 - Search box");
                        Console.WriteLine("3 - View your Shopping Cart");

                        foreach (var category in shopCategories)
                        {
                            Console.WriteLine($"{category.Id+3} - Choose Category {category.Name}");
                        }

                        Console.WriteLine("Q - Go back to the Start page");
                        Console.WriteLine("\nESC - Exits the Webshop");

                        ConsoleKeyInfo key = Console.ReadKey(true);

                        Console.Clear();

                        switch (key.Key)
                        {
                            case ConsoleKey.D1:
                                while (!loop)
                                {
                                    Console.WriteLine("Id".PadRight(5) + "\t" + "Name".PadRight(30) + "\t" + "Tags".PadRight(35) + "\t" + "Price".PadRight(15) + "\t" + "Stock".PadRight(10) + "\n");
                                    foreach (var item in shopItems)
                                    {
                                        Console.WriteLine($"{item.Id.ToString().PadRight(5)}\t{item.Name?.PadRight(30)}\t{item.Tag?[0]}, {item.Tag?[1]}, {item.Tag?[2].PadRight(20)}\t{item.Price.ToString().PadRight(15)}\t{item.Status} ({item.Inventory})");
                                    }
                                    Console.WriteLine("\n\nPlease type an item Id to add it to your Shopping Cart.");
                                    Console.WriteLine("\nType Q to go back to the menu");
                                    Console.WriteLine("\nType exit to Quit the Webshop\n");

                                    string? itemId = Console.ReadLine();

                                    if (int.TryParse(itemId, out int id))
                                    {
                                        var selectedItem = shopItems.FirstOrDefault(y => y.Id == id);

                                        if (selectedItem != null)
                                        {
                                            Console.Clear();
                                            selectedItem.Quantity++;
                                            selectedItem.Inventory--;
                                            ShoppingCart.sum += selectedItem.Price;
                                            Console.WriteLine($"Quantity of your {selectedItem.Name} is now: {selectedItem.Quantity}");
                                            Thread.Sleep(2000);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Item does not exist (please select 1-15).");
                                            Thread.Sleep(2000);
                                            Console.Clear();
                                        }
                                    }
                                    else if (itemId == "q")
                                    {
                                        var shopToFront = db.INL1Items.ToList();
                                        ItemStore.SetItems(shopToFront);
                                        loop = true;
                                    }
                                    else if (itemId == "exit")
                                    {
                                        loop = true;
                                        Program.exit = true;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Invalid Input, enter a number.");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }
                                }
                                break;

                            case ConsoleKey.D2:
                                while (!loop)
                                {
                                    bool itemFound = false;
                                    Console.WriteLine("Please Type a tag for the item you're looking for.");          // 5: Möjlighet att fritextsöka (DAPPER)

                                    string? searchString = Console.ReadLine();

                                    Console.WriteLine("\n");

                                    foreach (var item in shopItems)
                                    {
                                        foreach (var tag in item.Tag)
                                        {
                                            if (tag == searchString)
                                            {
                                                itemFound = true;
                                                Console.WriteLine($"{item.Id}: {item.Name}");
                                            }
                                        }
                                    }

                                    if (itemFound)
                                    {
                                        Console.WriteLine("\n\nSelect an Item Id to purchase that item. Press Enter to go back.");

                                        string? searchSelect = Console.ReadLine();

                                        Console.Clear();

                                        if (int.TryParse(searchSelect, out int id))
                                        {
                                            var selectSearch = shopItems.FirstOrDefault(m => m.Id == id);

                                            if (selectSearch != null)
                                            {
                                                Console.Clear();
                                                selectSearch.Quantity++;
                                                selectSearch.Inventory--;
                                                ShoppingCart.sum += selectSearch.Price;
                                                Console.WriteLine($"Quantity of your {selectSearch.Name} is now: {selectSearch.Quantity}");
                                                Thread.Sleep(2000);
                                                Console.Clear();
                                                loop = true;
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Item does not exist (please select 1-15).");
                                                Thread.Sleep(2000);
                                                Console.Clear();
                                            }
                                        }
                                        else if (searchSelect == "Enter")
                                        {
                                            Console.WriteLine("Returning to menu.");
                                            Thread.Sleep(2000);
                                            loop = true;
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("No items matched your search criteria.");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }
                                }
                                break;

                            case ConsoleKey.D3:
                                ItemStore.SetItems(shopItems);
                                ShoppingCart.Run();
                                break;

                            default:
                                categoryKey = int.Parse(key.KeyChar.ToString())-3;

                                Categories(categoryKey);
                                break;

                            case ConsoleKey.Q:
                                db.INL1Items.UpdateRange(shopItems);

                                quit = true;
                                break;

                            case ConsoleKey.Escape:
                                Program.exit = true;
                                break;
                            }
                            Console.Clear();
                        }
                        Console.Clear();
                    }
                }
            }

        public static void Categories(int category)
        {
            using (var db = new Models.MyDbContext())
            {
                var categoryItems = ItemStore.GetItems();

                bool quit = false;
                bool categoryFound = false;

                Console.Clear();

                while (!quit && !Program.exit)
                {
                    foreach (var item in categoryItems)
                    {
                        if (category == item.CategoryId)
                        {
                            categoryFound = true;
                            Console.WriteLine($"{categories[item.CategoryId-1].Name}: {item.Id} - {item.Name}");
                        }
                    }

                    if (categoryFound)
                    {
                        Console.WriteLine("\nPlease select an item Id to view more info. Type Q to go back or exit to quit the Webshop.\n");

                        string? itemId = Console.ReadLine();

                        if (int.TryParse(itemId, out int id))
                        {
                            var selectedItem = categoryItems.FirstOrDefault(y => y.Id == id);

                            if (selectedItem != null)
                            {
                                Console.Clear();
                                Console.WriteLine($"Id: {selectedItem.Id}");
                                Console.WriteLine($"Name: {selectedItem.Name}");
                                Console.WriteLine($"Description: {selectedItem.Description}");
                                Console.WriteLine($"Category: {selectedItem.Category?.Name}");
                                Console.WriteLine($"Tags: {selectedItem.Tag?[0]}, {selectedItem.Tag?[1]}, {selectedItem.Tag?[2]}");
                                Console.WriteLine($"Price: {selectedItem.Price}");
                                Console.WriteLine($"Supplier: {selectedItem.Supplier}");
                                Console.WriteLine($"Current Quantity: {selectedItem.Quantity}");
                                Console.WriteLine($"Webshop Inventory: {selectedItem.Inventory}");
                                string adminText = (selectedItem.IsSelectedByAdmin == true) ? "We recommend this item!" : "This item does not appear on our Front Page";
                                Console.WriteLine($"{adminText}");

                                Console.WriteLine("\n\nPress Enter to purchase item or any key to go back.");

                                ConsoleKeyInfo purchaseKey = Console.ReadKey(true);

                                Console.Clear();

                                if (purchaseKey.Key == ConsoleKey.Enter)
                                {
                                    Console.WriteLine("Item Added to your Shopping Cart.");
                                    selectedItem.Quantity++;
                                    selectedItem.Inventory--;
                                    ShoppingCart.sum += selectedItem.Price;
                                    Console.WriteLine($"\nQuantity of {selectedItem.Name} is now: {selectedItem.Quantity}");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    Console.WriteLine("Returning to Menu.");
                                    Thread.Sleep(2000);
                                }

                                Console.Clear();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Item does not exist (please select 1-15).");
                                Thread.Sleep(2000);
                                Console.Clear();
                            }
                        }
                        else if (itemId == "q")
                        {
                            ItemStore.SetItems(categoryItems);
                            quit = true;
                        }
                        else if (itemId == "exit")
                        {
                            quit = true;
                            Program.exit = true;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid Input, enter a number.");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Not a valid Category or Category does not contain any items.");
                        Thread.Sleep(2000);
                        Console.Clear();
                        quit = true;
                    }
                }
            }
        }
    }
}