using MenuTemplateForINL1.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MenuTemplateForINL1
{
    internal class Program
    {
        public static bool exit = false;                                                      // Local List for failsafe
        static async Task Main()
        {
            //Stopwatch sw = new();

            bool first = true;

            Console.CursorVisible = false;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);           // General settings for comfort before entering the Webshop

            using (var db = new Models.MyDbContext())
            {
                //Console.WriteLine("Fetching items from the database...\n");
                //sw.Start();
                //foreach (var item in await GetItems(db))
                //{
                //    db.INL1Items.Add(item);
                //}
                //sw.Stop();
                //Console.WriteLine($"Time to retrieve items: {sw.ElapsedMilliseconds}ms");              // One-time timer that shows time taken to asynchronously add db items to the local list
                //Thread.Sleep(2000);
                //Console.Clear();

                //var categories = new List<Category>
                //{
                //    new Category
                //    {
                //        Name = "drink"
                //    },
                //    new Category
                //    {
                //        Name = "beer"
                //    },
                //    new Category
                //    {
                //        Name = "special"
                //    }
                //};

                //db.INL1Categories.AddRange(categories);

                if (first)
                {
                    ShoppingPage.categories = db.INL1Categories.ToList();
                }

                first = false;

                //Category 1: Drink Glass Coasters
               // var items = new List<Item>
               //{
               //     new Item
               //     {
               //         Name = "Elegant Marble Coaster",
               //         Description = "A sleek and modern coaster made of pure marble.",
               //         Tag = new List<string> { "drink", "marble", "elegant" },
               //         Price = 120,
               //         Supplier = "MarbleCrafts",
               //         Quantity = 0,
               //         Inventory = 50,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 1,
               //         Category = ShoppingPage.categories[0],
               //     },

               //     new Item
               //     {
               //         Name = "Rustic Wooden Coaster",
               //         Description = "Crafted from reclaimed wood, perfect for a natural touch.",
               //         Tag = new List<string> { "drink", "wooden", "rustic" },
               //         Price = 80,
               //         Supplier = "EcoWare",
               //         Quantity = 0,
               //         Inventory = 75,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 1,
               //         Category = ShoppingPage.categories[0]
               //     },

               //     new Item
               //     {
               //         Name = "Ceramic Art Coaster",
               //         Description = "Hand-painted ceramic coaster, unique and colorful.",
               //         Tag = new List<string> { "drink", "ceramic", "handmade" },
               //         Price = 100,
               //         Supplier = "Artisan Creations",
               //         Quantity = 0,
               //         Inventory = 40,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 1,
               //         Category = ShoppingPage.categories[0]
               //     },

               //     new Item
               //     {
               //         Name = "Cork Classic Coaster",
               //         Description = "A lightweight and durable cork coaster.",
               //         Tag = new List<string> { "drink", "cork", "durable" },
               //         Price = 50,
               //         Supplier = "GreenCo",
               //         Quantity = 0,
               //         Inventory = 100,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 1,
               //         Category = ShoppingPage.categories[0]
               //     },

               //     new Item
               //     {
               //         Name = "Stone Slate Coaster",
               //         Description = "Rustic slate coaster with a natural finish.",
               //         Tag = new List<string> { "drink", "stone", "rustic" },
               //         Price = 110,
               //         Supplier = "NatureCraft",
               //         Quantity = 0,
               //         Inventory = 60,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 1,
               //         Category = ShoppingPage.categories[0]
               //     },

               //     //// Category 2: Beer Coasters
               //     new Item
               //     {
               //         Name = "Vintage Beer Coaster",
               //         Description = "Retro-style coaster with beer-themed prints.",
               //         Tag = new List<string> { "beer", "vintage", "retro" },
               //         Price = 70,
               //         Supplier = "BrewStyle",
               //         Quantity = 0,
               //         Inventory = 80,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 2,
               //         Category = ShoppingPage.categories[1]
               //     },

               //     new Item
               //     {
               //         Name = "Leather Beer Coaster",
               //         Description = "Premium leather coaster with embossed design.",
               //         Tag = new List<string> { "beer", "leather", "premium" },
               //         Price = 150,
               //         Supplier = "LuxCraft",
               //         Quantity = 0,
               //         Inventory = 30,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 2,
               //         Category = ShoppingPage.categories[1]
               //     },

               //     new Item
               //     {
               //         Name = "Silicone Beer Coaster",
               //         Description = "Non-slip silicone coaster, easy to clean.",
               //         Tag = new List<string> { "beer", "silicone", "clean" },
               //         Price = 60,
               //         Supplier = "TechStyle",
               //         Quantity = 0,
               //         Inventory = 120,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 2,
               //         Category = ShoppingPage.categories[1]
               //     },

               //     new Item
               //     {
               //         Name = "Bottle Cap Coaster",
               //         Description = "Fun coaster made from recycled bottle caps.",
               //         Tag = new List<string> { "beer", "recycled", "cap" },
               //         Price = 90,
               //         Supplier = "EcoCraft",
               //         Quantity = 0,
               //         Inventory = 50,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 2,
               //         Category = ShoppingPage.categories[1]
               //     },

               //     new Item
               //     {
               //         Name = "Metallic Beer Coaster",
               //         Description = "Stylish metallic coaster with a polished finish.",
               //         Tag = new List<string> { "beer", "metallic", "stylish" },
               //         Price = 130,
               //         Supplier = "ShinyCraft",
               //         Quantity = 0,
               //         Inventory = 40,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 2,
               //         Category = ShoppingPage.categories[1]
               //     },

               //     //// Category 3: Specialty Coasters
               //     new Item
               //     {
               //         Name = "Glow-in-the-Dark Coaster",
               //         Description = "A fun coaster that glows in the dark.",
               //         Tag = new List<string> { "special", "glow", "fun" },
               //         Price = 100,
               //         Supplier = "FunCraft",
               //         Quantity = 0,
               //         Inventory = 70,
               //         IsSelectedByAdmin = true,
               //         //CategoryId = 3,
               //         Category = ShoppingPage.categories[2]
               //     },

               //     new Item
               //     {
               //         Name = "Glass Coaster",
               //         Description = "See-Through Coaster made from ordinary glass.",
               //         Tag = new List<string> { "special", "glass", "clear" },
               //         Price = 200,
               //         Supplier = "CustomCraft",
               //         Quantity = 0,
               //         Inventory = 25,
               //         IsSelectedByAdmin = true,
               //         //CategoryId = 3,
               //         Category = ShoppingPage.categories[2]
               //     },

               //     new Item
               //     {
               //         Name = "Eco-Friendly Coaster",
               //         Description = "Sustainable coaster made from recycled materials.",
               //         Tag = new List<string> { "special", "eco", "sustainable" },
               //         Price = 90,
               //         Supplier = "GreenLife",
               //         Quantity = 0,
               //         Inventory = 80,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 3,
               //         Category = ShoppingPage.categories[2]
               //     },

               //     new Item
               //     {
               //         Name = "Geode Crystal Coaster",
               //         Description = "Unique coaster made from natural geode crystals.",
               //         Tag = new List<string> { "special", "crystal", "geode" },
               //         Price = 250,
               //         Supplier = "GemCraft",
               //         Quantity = 0,
               //         Inventory = 20,
               //         IsSelectedByAdmin = true,
               //         //CategoryId = 3,
               //         Category = ShoppingPage.categories[2]
               //     },

               //     new Item
               //     {
               //         Name = "Art Print Coaster",
               //         Description = "Coaster featuring prints of famous artworks.",
               //         Tag = new List<string> { "special", "art", "decorative" },
               //         Price = 110,
               //         Supplier = "ArtHouse",
               //         Quantity = 0,
               //         Inventory = 60,
               //         IsSelectedByAdmin = false,
               //         //CategoryId = 3,
               //         Category = ShoppingPage.categories[2]
               //     },
               //};

               // try
               // {
               //     db.INL1Items.AddRange(items);
               //     db.SaveChanges();
               // }
               // catch (Exception ex)
               // {
               //     Console.WriteLine(ex.InnerException?.Message);
               // }

                while (!exit)
                {
                    foreach (var item in await GetItems(db))
                    {
                        db.INL1Items.Add(item);
                    }

                    Console.WriteLine("Welcome to the Webshop! Here is a list of keyboard shortcuts you can use:\n");
                    Console.WriteLine("1 - Select the first item");
                    Console.WriteLine("2 - Select the second item");
                    Console.WriteLine("3 - Select the third item");
                    Console.WriteLine("4 - Enters the general shopping page");
                    Console.WriteLine("\nESC - Exits the Webshop");

                    Console.WriteLine("\n\n----------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n\n");
                    
                    int count = 0;
                    Item adminItem1 = new();
                    Item adminItem2 = new();
                    Item adminItem3 = new();

                    foreach (var item in db.INL1Items)
                    {
                        if (item.IsSelectedByAdmin)          // Load the selected items in the local list if they are selected by the admin
                        {
                            Console.CursorLeft = ((count) * 50);
                            Console.WriteLine($"{item.Name}: {item.Description}\n");
                            if (count == 0)
                            {
                                adminItem1 = item;
                            }
                            else if (count == 1)
                            {
                                adminItem2 = item;
                            }
                            else if (count == 2)
                            {
                                adminItem3 = item;
                            }
                            count++;
                        }
                    }

                    Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");

                    ConsoleKeyInfo key = Console.ReadKey(true);

                    Console.Clear();

                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine($"You have selected {adminItem1.Name}, {adminItem1.Description}. It costs {adminItem1.Price} and we currently have {adminItem1.Inventory} in stock.\n\n");
                            Console.WriteLine("Press Enter to purchase the product or Press any key to go back.");
                            ConsoleKeyInfo select1 = Console.ReadKey(true);
                            if (select1.Key == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                adminItem1.Quantity++;
                                adminItem1.Inventory--;
                                ShoppingCart.sum += adminItem1.Price;
                                Console.WriteLine($"Item added to Shopping Cart. Current Quantity: {adminItem1.Quantity}");

                                //itemList[adminItem1.Id-1].Quantity++;
                                //itemList[adminItem1.Id-1].Inventory--;
                                //ShoppingCart.sum += itemList[adminItem1.Id-1].Price;
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Returning to Main Menu.");
                                Thread.Sleep(2000);
                            }
                            break;

                        case ConsoleKey.D2:
                            Console.WriteLine($"You have selected {adminItem2.Name}, {adminItem2.Description}. It costs {adminItem2.Price} and we currently have {adminItem2.Inventory} in stock.\n\n");
                            Console.WriteLine("Press Enter to purchase the product or Press any key to go back.");
                            ConsoleKeyInfo select2 = Console.ReadKey(true);
                            if (select2.Key == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                adminItem2.Quantity++;
                                adminItem2.Inventory--;
                                ShoppingCart.sum += adminItem2.Price;
                                Console.WriteLine($"Item added to Shopping Cart. Current Quantity: {adminItem2.Quantity}");

                                //itemList[adminItem2.Id-1].Quantity++;
                                //itemList[adminItem2.Id-1].Inventory--;
                                //ShoppingCart.sum += itemList[adminItem2.Id-1].Price;
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Returning to Main Menu.");
                                Thread.Sleep(2000);
                            }
                            break;

                        case ConsoleKey.D3:
                            Console.WriteLine($"You have selected {adminItem3.Name}, {adminItem3.Description}. It costs {adminItem3.Price} and we currently have {adminItem3.Inventory} in stock.\n\n");
                            Console.WriteLine("Press Enter to purchase the product or Press any key to go back.");
                            ConsoleKeyInfo select3 = Console.ReadKey(true);
                            if (select3.Key == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                adminItem3.Quantity++;
                                adminItem3.Inventory--;
                                ShoppingCart.sum += adminItem3.Price;
                                Console.WriteLine($"Item added to Shopping Cart. Current Quantity: {adminItem3.Quantity}");

                                //itemList[adminItem3.Id-1].Quantity++;
                                //itemList[adminItem3.Id-1].Inventory--;
                                //ShoppingCart.sum += itemList[adminItem3.Id-1].Price;
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Returning to Main Menu.");
                                Thread.Sleep(2000);
                            }
                            break;

                        case ConsoleKey.D4:
                            var frontToShop = db.INL1Items.ToList();
                            ItemStore.SetItems(frontToShop);
                            ShoppingPage.Run();
                            break;

                        case ConsoleKey.A:
                            var frontToAdmin = db.INL1Items.ToList();
                            ItemStore.SetItems(frontToAdmin);
                            string? pw = Console.ReadLine();
                            if (pw == "admin")
                            {
                                Admin.Run();
                            }
                            break;

                        case ConsoleKey.Escape:
                            exit = true;
                            break;
                    }
                    Console.Clear();
                }
                Console.WriteLine("Come back anytime!");
            }
        }

        public static async Task<List<Models.Item>> GetItems(MyDbContext db)
        {
            var items = await db.INL1Items.ToListAsync();

            return items;
        }
    }
}