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
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace MenuTemplateForINL1.Models
{
    internal class Admin
    {
        public static string connString = ("Server = tcp:gimlidb.database.windows.net, 1433; Initial Catalog = FreeDB; Persist Security Info = False; User ID = gimli117;" +
                "Password =DrunkDwarf117; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = True;");                                                           //Dapper
        public static void Run()
        {
            bool quit = false;

            while (!quit && !Program.exit)
            {
                Console.Clear();

                Console.WriteLine("Admin Page. Select a Command\n\n");
                Console.WriteLine("1 - Manage Items");
                Console.WriteLine("2 - Add an Item");
                Console.WriteLine("3 - Manage Categories");
                Console.WriteLine("4 - Add a Category");
                Console.WriteLine("5 - Manage Customers");
                Console.WriteLine("6 - View Order History");
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
                        CreateItem();
                        break;

                    case ConsoleKey.D3:
                        ManageCategories();
                        break;

                    case ConsoleKey.D4:
                        CreateCategory();
                        break;

                    case ConsoleKey.D5:
                        ManageCustomers();
                        break;

                    case ConsoleKey.D6:
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
                var manageItems = ItemStore.GetItems();
                while (!quit && !Program.exit)
                {
                    bool goBack = false;

                    Console.WriteLine("Item Management. Choose an item from the list to edit. Type Q to go back or type exit to quit. Type S to save changes.\n");
                    Console.WriteLine("Listing all items here...\n");

                    Console.WriteLine("Id".PadRight(5) + "\t" + "Name".PadRight(30) + "\t" + "Category".PadRight(10) + "\t" + "Tags".PadRight(35) + "\t" + "Price".PadRight(15) + "\t" + "Stock".PadRight(10) + "\t\t" + "IsSelected" + "\n");
                    foreach (var item in manageItems)
                    {
                        Console.WriteLine($"{item.Id.ToString().PadRight(5)}\t{item.Name?.PadRight(30)}\t{item.CategoryId.ToString()}, {ShoppingPage.categories[item.CategoryId-1].Name?.PadRight(10)}\t{item.Tag?[0]}, {item.Tag?[1]}, {item.Tag?[2].PadRight(20)}\t{item.Price.ToString().PadRight(15)}\t{item.Status} ({item.Inventory})\t\t{item.IsSelectedByAdmin}");
                    }

                    Console.WriteLine();

                    string? selectedId = Console.ReadLine();

                    if (int.TryParse(selectedId, out int id))
                    {
                        var selectedItem = manageItems.FirstOrDefault(i => i.Id == id);

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
                            Console.WriteLine($"8 - Category: {selectedItem.CategoryId}");
                            Console.WriteLine("\nQ - Go back to the menu: ");
                            Console.WriteLine("ESC - Exits the Webshop\n");

                            ConsoleKeyInfo key = Console.ReadKey(true);

                            Console.Clear();

                            switch (key.Key)
                            {
                                case ConsoleKey.D1:                                                                     // Name
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
                                        //Program.itemList[selectedItem.Id-1].Name = newName;
                                    }
                                    break;

                                case ConsoleKey.D2:                                                                     // Description
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
                                        //Program.itemList[selectedItem.Id-1].Description = newDescription;
                                    }
                                    break;

                                case ConsoleKey.D3:                                                                     // Tags
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
                                                    //Program.itemList[selectedItem.Id].Tag[0] = newTag;
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
                                                    //Program.itemList[selectedItem.Id].Tag[1] = newTag;
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
                                                    //Program.itemList[selectedItem.Id].Tag[2] = newTag;
                                                }
                                                break;

                                            case ConsoleKey.X:
                                                goBack = true;
                                                break;
                                        }
                                    }
                                    break;

                                case ConsoleKey.D4:                                                                     // Price
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
                                        //Program.itemList[selectedItem.Id-1].Price = price;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Invalid Input.");
                                        Thread.Sleep(2000);
                                    }
                                    break;

                                case ConsoleKey.D5:                                                                     // Supplier
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
                                        //Program.itemList[selectedItem.Id-1].Supplier = newSupplier;
                                    }
                                    break;

                                case ConsoleKey.D6:                                                                     // Inventory
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
                                        //Program.itemList[selectedItem.Id-1].Inventory = inventory;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Invalid Input.");
                                        Thread.Sleep(2000);
                                    }
                                    break;

                                case ConsoleKey.D7:                                                                     // IsSelectedByAdmin
                                    Console.WriteLine($"Currently selected: {selectedItem.IsSelectedByAdmin}\n\n");
                                    Console.WriteLine("Select or Deselect this item\n");
                                    Console.WriteLine("Press Enter to invert this setting");
                                    Console.WriteLine("Press any key to keep this setting");
                                    ConsoleKeyInfo select = Console.ReadKey(true);

                                    int count = 0;

                                    if (select.Key == ConsoleKey.Enter)
                                    {
                                        foreach (var item in manageItems)
                                        {
                                            if (item.IsSelectedByAdmin)
                                            {
                                                count++;
                                            }
                                        }
                                        if (selectedItem.IsSelectedByAdmin)
                                        {
                                            selectedItem.IsSelectedByAdmin = false;
                                            //Program.itemList[selectedItem.Id-1].IsSelectedByAdmin = false;
                                        }
                                        else if (!selectedItem.IsSelectedByAdmin && count < 3)                      // Maximum of 3 items on the Front Page
                                        {
                                            selectedItem.IsSelectedByAdmin = true;
                                            //Program.itemList[selectedItem.Id-1].IsSelectedByAdmin = true;
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

                                case ConsoleKey.D8:                                                                 // Category
                                    Console.WriteLine("Listing all existing Categories.\n\n");
                                    foreach (var category in ShoppingPage.categories)
                                    {
                                        Console.WriteLine($"{category.Id}: {category.Name}");
                                    }

                                    Console.WriteLine($"\n\nCurrent Category: {selectedItem.CategoryId} - {selectedItem.Category?.Name}\n");
                                    Console.WriteLine("Enter new Category\n");
                                    string? newCategory = Console.ReadLine();
                                    if (newCategory == "")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Category wasn't changed.");
                                        Thread.Sleep(2000);
                                    }
                                    else if (int.TryParse(newCategory, out int category))
                                    {
                                        var selectedCategory = ShoppingPage.categories.FirstOrDefault(c => c.Id == id);

                                        if (selectedCategory != null)
                                        {
                                            selectedItem.CategoryId = id;
                                            selectedItem.Category = ShoppingPage.categories[id - 1];
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Category does not exist.\n\n");
                                            Console.WriteLine("Do you want to create a new Category? Enter to continue or any key to go back.");

                                            ConsoleKeyInfo categoryKey = Console.ReadKey(true);

                                            if (key.Key == ConsoleKey.Enter)
                                            {
                                                CreateCategory();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Invalid Input.");
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
                            Console.WriteLine("Item does not exist.\n\n");
                            Console.WriteLine("Do you want to create a new Item? Enter to continue or any key to go back.");

                            ConsoleKeyInfo key = Console.ReadKey(true);

                            if (key.Key == ConsoleKey.Enter)
                            {
                                CreateItem();
                            }
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
                        db.INL1Items.UpdateRange(manageItems);

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
                            Console.WriteLine(ex.InnerException?.Message);
                        }

                        ItemStore.SetItems(manageItems);
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

        public static void CreateItem()
        {
            bool itemExists = false;
            bool loop = true;
            bool intLoop = true;
            int itemPrice = 0;
            int itemInventory = 0;
            int itemCategoryId = 0;

            using (var db = new Models.MyDbContext())
            {
                var addItem = ItemStore.GetItems();
                while (loop)
                {
                    Console.Clear();

                    Console.WriteLine("Let's add an Item to your Webshop!\n\n");
                    Console.WriteLine("Please enter the new Item Name:\n");

                    string? itemName = Console.ReadLine();

                    foreach (var item in addItem)
                    {
                        if (item.Name == itemName)
                        {
                            itemExists = true;
                        }
                    }

                    if (itemExists)
                    {
                        Console.Clear();
                        Console.WriteLine("Item already exists.");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter the new Item Description:\n");
                        string? itemDescription = Console.ReadLine();

                        Console.Clear();
                        Console.WriteLine("Please enter the new Item Tags:\n\n");
                        Console.Write("Tag 1: ");
                        string? itemTag1 = Console.ReadLine();
                        Console.Write("\n\nTag 2: ");
                        string? itemTag2 = Console.ReadLine();
                        Console.Write("\n\nTag 3: ");
                        string? itemTag3 = Console.ReadLine();

                        while (intLoop)
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter the new Item Price:\n");
                            string? tryItemPrice = Console.ReadLine();
                            if (int.TryParse(tryItemPrice, out var price))
                            {
                                itemPrice = price;
                                intLoop = false;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter a number...");
                                Thread.Sleep(2000);
                            }
                        }

                        Console.Clear();
                        Console.WriteLine("Please enter the new Item Supplier:\n");
                        string? itemSupplier = Console.ReadLine();

                        intLoop = true;

                        while (intLoop)
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter the new Item Inventory:\n");
                            string? tryItemInventory = Console.ReadLine();
                            if (int.TryParse(tryItemInventory, out var inventory))
                            {
                                itemInventory = inventory;
                                intLoop = false;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter a number...");
                                Thread.Sleep(2000);
                            }
                        }

                        intLoop = true;

                        while (intLoop)
                        {
                            Console.Clear();
                            foreach (var category in ShoppingPage.categories)
                            {
                                Console.WriteLine($"Id: {category.Id} ({category.Name})");
                            }
                            Console.WriteLine("\n\nPlease enter the new Item Category Id:\n");
                            string? tryItemCategory = Console.ReadLine();

                            if (int.TryParse(tryItemCategory, out var categoryItem))
                            {
                                var selectedCategory = ShoppingPage.categories.FirstOrDefault(c => c.Id == categoryItem);

                                if (selectedCategory != null)
                                {
                                    itemCategoryId = selectedCategory.Id;
                                    intLoop = false;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Category does not exist...");
                                    Thread.Sleep(2000);
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter a number...");
                                Thread.Sleep(2000);
                            }
                        }

                        Console.Clear();

                        var newitem = new Item
                        {
                            Name = itemName,
                            Description = itemDescription,
                            Tag = new List<string> { itemTag1, itemTag2, itemTag3 },
                            Price = itemPrice,
                            Supplier = itemSupplier,
                            Quantity = 0,
                            Inventory = itemInventory,
                            IsSelectedByAdmin = false,
                            CategoryId = itemCategoryId,
                            Category = ShoppingPage.categories[itemCategoryId-1],
                        };

                        ItemStore.AddItem(newitem);
                        db.INL1Items.Add(newitem);

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.InnerException?.Message);
                        }

                        Console.WriteLine($"Item {itemName} added.");
                        Thread.Sleep(2000);
                        loop = false;
                    }
                }
            }
        }

        public static void ManageCategories()
        {
            bool quit = false;

            while (!quit && !Program.exit)
            {
                Console.Clear();

                Console.WriteLine("Category Management. Listing all Categories below. Type Q to go back or type exit to leave the Webshop.\n\n");

                foreach (var category in ShoppingPage.categories)
                {
                    Console.WriteLine($"{category.Id}: {category.Name}");
                }
                Console.WriteLine("\n\nSelect a Category Id to change its name.");

                string? selectedCategory = Console.ReadLine();

                if (int.TryParse(selectedCategory, out int id))
                {
                    var categorySelected = ShoppingPage.categories.FirstOrDefault(i => i.Id == id);

                    if (categorySelected != null)
                    {
                        Console.WriteLine($"Current Category Name: {ShoppingPage.categories[id-1].Name}\n");
                        Console.WriteLine("Enter new Category Name\n");
                        string? newCategory = Console.ReadLine();
                        if (newCategory == "")
                        {
                            Console.Clear();
                            Console.WriteLine("Category wasn't changed.");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            ShoppingPage.categories[id-1].Name = newCategory;
                            //Program.itemList[selectedItem.Id-1].Supplier = newSupplier;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Category does not exist.");
                        Thread.Sleep(2000);
                    }
                }
                else if (selectedCategory == "q")
                {
                    quit = true;
                }
                else if (selectedCategory == "exit")
                {
                    quit = true;
                    Program.exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input, enter a number.");
                    Thread.Sleep(2000);
                }
            }
        }

        public static void CreateCategory()
        {
            bool categoryExists = false;
            bool loop = true;

            using (var db = new Models.MyDbContext())
            {
                while (loop)
                {
                    Console.Clear();

                    Console.WriteLine("Let's add a Category to your Webshop!\n\n");
                    Console.WriteLine("Please enter the new Category Name:\n");

                    string? categoryName = Console.ReadLine();

                    foreach (var category in ShoppingPage.categories)
                    {
                        if (category.Name == categoryName)
                        {
                            categoryExists = true;
                        }
                    }

                    if (categoryExists)
                    {
                        Console.Clear();
                        Console.WriteLine("Category already exists.");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Category {categoryName} added.");

                        ShoppingPage.categories.Add(new Category { Name = categoryName });
                        db.INL1Categories.Add(new Category { Name = categoryName });

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.InnerException?.Message);
                        }

                        Thread.Sleep(2000);
                        loop = false;
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

                    foreach (var customer in db.INL1Customers)
                    {
                        Console.WriteLine($"{customer.Id}: {customer.Name}");
                    }


                    Console.WriteLine();
                    string? selectedCustomer = Console.ReadLine();

                    if (int.TryParse(selectedCustomer, out int id1))
                    {
                        var customerSelected = db.INL1Customers.FirstOrDefault(n => n.Id == id1);
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
                    foreach (var order in db.INL1PreviousOrders)
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