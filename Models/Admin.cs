using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using System.ComponentModel.Design;

namespace MenuTemplateForINL1.Models
{
    internal class Admin
    {
        public static string connString = "data source=.\\SQLEXPRESS; initial catalog = WebshopDemo; TrustServerCertificate=true; persist security info = True; Integrated Security = True;";   //Dapper
        public static void Run()
        {
            bool quit = false;

            while (!quit && !Program.exit)
            {
                Console.Clear();

                Console.WriteLine("Admin Page. Select a Command\n\n");
                Console.WriteLine("1 - Manage Items");
                Console.WriteLine("2 - Manage Customers");
                Console.WriteLine("3 - View Order History");
                Console.WriteLine("\nQ - Go back to the Start Page");
                Console.WriteLine("ESC - Exits the Webshop");

                ConsoleKeyInfo key = Console.ReadKey(true);

                Console.Clear();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        ManageItems();
                        break;

                    case ConsoleKey.D2:
                        ManageCustomers();
                        break;

                    case ConsoleKey.D3:
                        ViewOrders();
                        break;

                    case ConsoleKey.Q:
                        quit = true;
                        break;

                    case ConsoleKey.Escape:
                        quit = true;
                        Program.exit = true;
                        break;
                }
            }
        }

        public static void ManageItems()
        {
            bool quit = false;

            using (var db = new Models.MyDbContext())
            {
                while (!quit && !Program.exit)
                {
                    bool goBack = false;

                    Console.WriteLine("Item Management. Choose an item from the list to edit. Type Q to go back or type exit to quit. Type S to save changes.\n");
                    Console.WriteLine("Listing all items here...\n");

                    foreach (var item in Program.itemList)
                    {
                        Console.WriteLine($"{item.Id}: {item.Name}");
                    }

                    Console.WriteLine();

                    string? selectedId = Console.ReadLine();

                    if (int.TryParse(selectedId, out int id))
                    {
                        var selectedItem = db.Items.FirstOrDefault(x => x.Id == id);

                        if (selectedItem != null)
                        {
                            Console.Clear();

                            Console.WriteLine($"Select what you would like to change:\n\n");
                            Console.WriteLine($"1 - Name: {selectedItem.Name}");
                            Console.WriteLine($"2 - Description: {selectedItem.Description}");
                            Console.WriteLine($"3 - Tag: Select to view all tags.");
                            Console.WriteLine($"4 - Price: {selectedItem.Price}");
                            Console.WriteLine($"5 - Supplier: {selectedItem.Supplier}");
                            Console.WriteLine($"6 - Inventory: {selectedItem.Inventory}");
                            Console.WriteLine($"7 - IsSelectedByAdmin: {selectedItem.IsSelectedByAdmin}");
                            Console.WriteLine("\nQ - Go back to the menu: ");
                            Console.WriteLine("ESC - Exits the Webshop\n");

                            ConsoleKeyInfo key = Console.ReadKey(true);

                            Console.Clear();

                            switch (key.Key)
                            {
                                case ConsoleKey.D1:                                                                 // Name
                                    Console.WriteLine($"Current Name: {selectedItem.Name}\n");
                                    Console.WriteLine("Enter new Name. Press Enter to go back.\n");
                                    string? newName = Console.ReadLine();
                                    if (newName == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Name wasn't changed.");
                                        Thread.Sleep(2000);
                                    }
                                    else
                                    {
                                        selectedItem.Name = newName;
                                        Program.itemList[selectedItem.Id].Name = newName;
                                    }
                                    break;

                                case ConsoleKey.D2:                                                                 // Description
                                    Console.WriteLine($"Current Description: {selectedItem.Description}\n");
                                    Console.WriteLine("Enter new Description. Press Enter to go back.\n");
                                    string? newDescription = Console.ReadLine();
                                    if (newDescription == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Description wasn't changed.");
                                        Thread.Sleep(2000);
                                    }
                                    else
                                    {
                                        selectedItem.Description = newDescription;
                                        Program.itemList[selectedItem.Id].Description = newDescription;
                                    }
                                    break;

                                case ConsoleKey.D3:                                                                 // Tag
                                    while (!goBack)
                                    {
                                        Console.Clear();

                                        Console.WriteLine("Current Tags: \n\n");
                                        int i = 1;
                                        foreach (var tag in selectedItem.Tag)
                                        {
                                            Console.WriteLine($"{i}: {tag}");
                                            i++;
                                        }
                                        i = 1;
                                        Console.WriteLine("\n\nSelect a tag to manage it. Press X to quit.\n");

                                        ConsoleKeyInfo tagSelect = Console.ReadKey(true);

                                        Console.Clear();

                                        switch (tagSelect.Key)
                                        {
                                            case ConsoleKey.D1:
                                                Console.WriteLine($"Current Tag 1: {selectedItem.Tag[0]}\n");
                                                Console.WriteLine("Enter new Tag 1\n");
                                                string? newTag = Console.ReadLine();
                                                if (newTag == "")
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Tag 1 wasn't changed.");
                                                    Thread.Sleep(2000);
                                                }
                                                else
                                                {
                                                    selectedItem.Tag[0] = newTag;
                                                    Program.itemList[selectedItem.Id].Tag[0] = newTag;
                                                }
                                                break;

                                            case ConsoleKey.D2:
                                                Console.WriteLine($"Current Tag 2: {selectedItem.Tag[1]}\n");
                                                Console.WriteLine("Enter new Tag 2\n");
                                                newTag = Console.ReadLine();
                                                if (newTag == "")
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Tag 2 wasn't changed.");
                                                    Thread.Sleep(2000);
                                                }
                                                else
                                                {
                                                    selectedItem.Tag[1] = newTag;
                                                    Program.itemList[selectedItem.Id].Tag[1] = newTag;
                                                }
                                                break;

                                            case ConsoleKey.D3:
                                                Console.WriteLine($"Current Tag 3: {selectedItem.Tag[2]}\n");
                                                Console.WriteLine("Enter new Tag 3\n");
                                                newTag = Console.ReadLine();
                                                if (newTag == "")
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Tag 3 wasn't changed.");
                                                    Thread.Sleep(2000);
                                                }
                                                else
                                                {
                                                    selectedItem.Tag[2] = newTag;
                                                    Program.itemList[selectedItem.Id].Tag[2] = newTag;
                                                }
                                                break;

                                            case ConsoleKey.X:
                                                goBack = true;
                                                break;
                                        }
                                    }
                                    break;

                                case ConsoleKey.D4:                                                                 // Price
                                    Console.WriteLine($"Current Price: {selectedItem.Price}\n");
                                    Console.WriteLine("Enter new Price. Press Enter to go back.\n");
                                    string? newPrice = Console.ReadLine();
                                    if (newPrice == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Price wasn't changed.");
                                        Thread.Sleep(2000);
                                    }
                                    else if (int.TryParse(newPrice, out int price))
                                    {
                                        selectedItem.Price = price;
                                        Program.itemList[selectedItem.Id].Price = price;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Invalid Input.");
                                        Thread.Sleep(2000);
                                    }
                                    break;

                                case ConsoleKey.D5:                                                                 // Supplier
                                    Console.WriteLine($"Current Supplier: {selectedItem.Supplier}\n");
                                    Console.WriteLine("Enter new Supplier\n");
                                    string? newSupplier = Console.ReadLine();
                                    if (newSupplier == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Supplier wasn't changed.");
                                        Thread.Sleep(2000);
                                    }
                                    else
                                    {
                                        selectedItem.Supplier = newSupplier;
                                        Program.itemList[selectedItem.Id].Supplier = newSupplier;
                                    }
                                    break;

                                case ConsoleKey.D6:                                                                 // Inventory
                                    Console.WriteLine($"Current Inventory: {selectedItem.Inventory}\n");
                                    Console.WriteLine("Enter new Inventory\n");
                                    string? newInventory = Console.ReadLine();
                                    if (newInventory == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Inventory wasn't changed.");
                                        Thread.Sleep(2000);
                                    }
                                    else if (int.TryParse(newInventory, out int inventory))
                                    {
                                        selectedItem.Inventory = inventory;
                                        Program.itemList[selectedItem.Id].Inventory = inventory;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Invalid Input.");
                                        Thread.Sleep(2000);
                                    }
                                    break;

                                case ConsoleKey.D7:                                                                 // IsSelectedByAdmin
                                    Console.WriteLine($"Currently selected: {selectedItem.IsSelectedByAdmin}\n\n");
                                    Console.WriteLine("Select or Deselect this item\n");
                                    Console.WriteLine("Press Enter to invert this setting");
                                    Console.WriteLine("Press any key to keep this setting");
                                    ConsoleKeyInfo select = Console.ReadKey(true);

                                    int count = 0;

                                    if (select.Key == ConsoleKey.Enter)
                                    {
                                        foreach (var item in Program.itemList)
                                        {
                                            if (item.IsSelectedByAdmin)
                                            {
                                                count++;
                                            }
                                        }
                                        if (selectedItem.IsSelectedByAdmin)
                                        {
                                            selectedItem.IsSelectedByAdmin = false;
                                            Program.itemList[selectedItem.Id - 1].IsSelectedByAdmin = false;
                                        }
                                        else if (!selectedItem.IsSelectedByAdmin && count < 3)
                                        {
                                            selectedItem.IsSelectedByAdmin = true;
                                            Program.itemList[selectedItem.Id - 1].IsSelectedByAdmin = true;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("3 items selected already.");
                                            Thread.Sleep(2000);
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Item selected by Admin wasn't changed.");
                                        Thread.Sleep(2000);
                                    }

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
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Item does not exist (please select 1-15).");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                    }
                    else if (selectedId == "q")
                    {
                        quit = true;
                    }
                    else if (selectedId == "exit")
                    {
                        quit = true;
                        Program.exit = true;
                    }
                    else if (selectedId == "s")                                         // Admin is able to go back without saving changes
                    {
                        try
                        {
                            Console.Clear();
                            db.SaveChanges();
                            Console.WriteLine("Changes saved.");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.InnerException.Message);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Input, enter a number.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                }
            }
        }

        public static void ManageCustomers()
        {
            bool quit = false;

            using (var db = new Models.MyDbContext())
            {
                while (!quit && !Program.exit)
                {
                    Console.Clear();

                    Console.WriteLine("Listing all customers that have made purchases. Select a Customer to manage. Type Q to quit or type exit to Leave the Webshop.\n\n");

                    foreach (var customer in db.Customers)
                    {
                        Console.WriteLine($"{customer.Id}: {customer.Name}");
                    }


                    Console.WriteLine();
                    string? selectedCustomer = Console.ReadLine();

                    if (int.TryParse(selectedCustomer, out int id1))
                    {
                        var customerSelected = db.Customers.FirstOrDefault(n => n.Id == id1);
                        if (customerSelected != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"Managing Customer {customerSelected.Id}: {customerSelected.Name}");
                            Console.WriteLine($"\nCity: {customerSelected.City}");
                            Console.WriteLine($"Postal Code: {customerSelected.Postal}");
                            Console.WriteLine($"Street: {customerSelected.Street}");
                            Console.WriteLine("\n\n1 - Update info of this customer");
                            Console.WriteLine("X - Go back to the menu");

                            ConsoleKeyInfo customerKey = Console.ReadKey(true);

                            Console.Clear();

                            switch (customerKey.Key)
                            {
                                case ConsoleKey.D1:
                                    Console.WriteLine($"Enter new Customer Name (Current: {customerSelected.Name}), or press Enter to keep");
                                    string? newName = Console.ReadLine();
                                    if (newName == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Name wasn't changed.");
                                        newName = customerSelected.Name;
                                        Thread.Sleep(2000);
                                    }

                                    Console.Clear();
                                    Console.WriteLine($"Enter new City (Current: {customerSelected.City}), or press Enter to keep");
                                    string? newCity = Console.ReadLine();
                                    if (newCity == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("City wasn't changed.");
                                        newCity = customerSelected.City;
                                        Thread.Sleep(2000);
                                    }

                                    Console.Clear();
                                    Console.WriteLine($"Enter new Postal Code (Current: {customerSelected.Postal}), or press Enter to keep");
                                    string? newPostal = Console.ReadLine();
                                    if (newPostal == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Postal Code wasn't changed.");
                                        newPostal = customerSelected.Postal;
                                        Thread.Sleep(2000);
                                    }

                                    Console.Clear();
                                    Console.WriteLine($"Enter new Street (Current: {customerSelected.Street}), or press Enter to keep");
                                    string? newStreet = Console.ReadLine();
                                    if (newStreet == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Street wasn't changed.");
                                        newStreet = customerSelected.Street;
                                        Thread.Sleep(2000);
                                    }
                                        
                                    UpdateCustomer(customerSelected.Id, newName, newCity, newPostal, newStreet);    // Update Database using but also update local values
                                    customerSelected.Name = newName;
                                    customerSelected.City = newCity;
                                    customerSelected.Postal = newPostal;
                                    customerSelected.Street = newStreet;
                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Customer does not exist.");
                            Thread.Sleep(2000);
                        }
                    }
                    else if (selectedCustomer == "q")
                    {
                        quit = true;
                    }
                    else if (selectedCustomer == "exit")
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
            }
        }        

        public static void UpdateCustomer(int customerId, string? name, string? city, string? postal, string? street)
        {
            using (var connection = new SqlConnection(connString))
            {
                string query = @"
            UPDATE Customers 
            SET Name = @Name, City = @City, Postal = @Postal, Street = @Street 
            WHERE Id = @Id";

                connection.Execute(query, new
                {
                    Id = customerId,
                    Name = name,
                    City = city,
                    Postal = postal,
                    Street = street
                });
            }
        }

        public static void ViewOrders()
        {
            bool quit = false;

            using (var db = new Models.MyDbContext())
            {
                while (!quit && !Program.exit)
                {
                    foreach (var order in db.PreviousOrders)
                    {
                        Console.WriteLine($"Id {order.Id}: Order Number {order.OrderNum} - Customer {order.CustomerId}");
                    }
                    Console.WriteLine("\n\nShowing all Previous Orders. Press any key to go back or press Escape to Exit the Webshop.");

                    ConsoleKeyInfo orderKey = Console.ReadKey(true);                    

                    if (orderKey.Key == ConsoleKey.Escape)
                    {
                        quit = true;
                        Program.exit = true;
                    }
                    else
                    {
                        quit = true;
                    }
                }
            }
        }
    }
}