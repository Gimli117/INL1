using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MenuTemplateForINL1.Models
{
    internal class ShoppingCart
    {
        public static int sum;
        public static void Run()
        {
            bool quit = false;

            using (var db = new Models.MyDbContext())
            {
                while (!quit && !Program.exit)
                {
                    Console.WriteLine("This is your Shopping Cart. Your items are listed below:\n\n");

                    int shopCartIndex = 0;

                    foreach (var item in Program.itemList)                                                   // 7: Produkterna visas i lista
                    {
                        if (Program.itemList[shopCartIndex].Quantity > 0)
                        {
                            Console.WriteLine($"Item{Program.itemList[shopCartIndex].Id}: {Program.itemList[shopCartIndex].Name}, {Program.itemList[shopCartIndex].Quantity} pieces - {Program.itemList[shopCartIndex].Price * Program.itemList[shopCartIndex].Quantity}kr");
                        }
                        shopCartIndex++;
                    }
                    Console.WriteLine($"\nTotal price: {sum}");                                               // 10: Priset och summan visas     

                    Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");

                    Console.WriteLine("1 - Change quantity of one of your items");                           // 8,9: Möjlighet att ändra antal och ta bort en produkt
                    Console.WriteLine("2 - Continue to Checkout");
                    Console.WriteLine("Q - Go back to the Shopping Page");
                    Console.WriteLine("ESC - Exits the Webshop");

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.D1:
                            AddToCart();
                            break;

                        case ConsoleKey.D2:
                            ShippingNPayment.Shipping();
                            break;

                        case ConsoleKey.Q:
                            quit = true;
                            break;

                        case ConsoleKey.Escape:
                            quit = true;
                            Program.exit = true;
                            break;
                    }
                    Console.Clear();
                }
            }
        }

        public static void AddToCart()
        {
            bool goBack = false;
            bool loop = true;

            while (!goBack)
            {
                Console.Clear();

                for (int i = 0; i < Program.itemList.Count; i++)
                {
                    if (Program.itemList[i].Quantity > 0)
                    {
                        Console.WriteLine($"{Program.itemList[i].Id}: {Program.itemList[i].Name}, {Program.itemList[i].Quantity} pieces - {Program.itemList[i].Price * Program.itemList[i].Quantity}kr");
                    }
                }

                Console.WriteLine("\nTo begin with, choose which item to change quantity. Type Q to go back to the Shopping Cart\n");

                string? selectedId = Console.ReadLine();

                if (int.TryParse(selectedId, out int id))
                {
                    var itemSelect = Program.itemList.FirstOrDefault(z => z.Id == id);

                    if (itemSelect.Quantity > 0)
                    {
                        int inventoryMax = itemSelect.Inventory + itemSelect.Quantity;
                        while (loop)
                        {
                            Console.Clear();

                            Console.WriteLine($"You have selected {itemSelect.Name} which you currently have {itemSelect.Quantity} of. {itemSelect.Inventory} left in stock.\n");
                            Console.WriteLine("1 - Increase Quantity of item.");
                            Console.WriteLine("2 - Decrease Quantity of item.");
                            Console.WriteLine($"3 - Remove all {itemSelect.Name} from your Shopping Cart.");
                            Console.WriteLine("X - Go back to Item Selection.");

                            ConsoleKeyInfo keySelect = Console.ReadKey(true);

                            Console.Clear();

                            switch (keySelect.Key)
                            {
                                case ConsoleKey.D1:
                                    if (itemSelect.Quantity < inventoryMax)
                                    {
                                        itemSelect.Quantity++;
                                        itemSelect.Inventory--;
                                        sum += itemSelect.Price;
                                        Console.WriteLine("Quantity increased.");
                                        Thread.Sleep(2000);
                                    }
                                    else if (itemSelect.Quantity == inventoryMax)
                                    {
                                        itemSelect.Quantity++;
                                        itemSelect.Inventory = 0;
                                        sum += itemSelect.Price;
                                        Console.WriteLine("The item is now sold out!");
                                        Thread.Sleep(2000);
                                    }
                                    break;

                                case ConsoleKey.D2:
                                    if (itemSelect.Quantity > 1)
                                    {
                                        itemSelect.Quantity--;
                                        itemSelect.Inventory++;
                                        sum -= itemSelect.Price;
                                        Console.WriteLine("Quantity decreased.");
                                        Thread.Sleep(2000);
                                    }
                                    else if (itemSelect.Quantity == 1)
                                    {
                                        itemSelect.Quantity = 0;
                                        itemSelect.Inventory++;
                                        sum -= itemSelect.Price;
                                        Console.WriteLine("Item removed.");
                                        Thread.Sleep(2000);
                                    }
                                    break;

                                case ConsoleKey.D3:
                                    sum -= (itemSelect.Price * itemSelect.Quantity);
                                    itemSelect.Inventory = inventoryMax;
                                    itemSelect.Quantity = 0;
                                    Console.WriteLine("Item removed.");
                                    Thread.Sleep(2000);
                                    break;

                                case ConsoleKey.X:
                                    loop = false;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please select an item Id from your Shopping Cart.");
                        Thread.Sleep(2000);
                    }
                }
                else if (selectedId == "q")
                {
                    goBack = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input, enter a number.");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}