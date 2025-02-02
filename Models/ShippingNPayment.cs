using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MenuTemplateForINL1.Models
{
    internal class ShippingNPayment
    {
        public static CardPaymentInfo newCardPaymentInfo = new CardPaymentInfo();
        public static void Shipping()
        {
            bool quit = false;
            int shippingMethod;

            using (var db = new Models.MyDbContext())
            {
                while (!quit && !Program.exit)
                {
                    bool choice = true;

                    Console.Clear();

                    Console.WriteLine("Shipping Details before payment for your order\n");
                    Console.WriteLine("Press Enter to continue");
                    Console.WriteLine("Q - Go back to the Shopping Cart");
                    Console.WriteLine("ESC - Exits the Webshop");

                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.Q:
                            quit = true;
                            break;

                        case ConsoleKey.Escape:
                            quit = true;
                            Program.exit = true;
                            break;

                        default:
                            Console.Clear();

                            Console.WriteLine("Enter your name:");                                      // 11: Namn och adress ska gå att fylla i
                            string? name = Console.ReadLine();
                            Console.Clear();

                            Console.WriteLine("Enter your City:");
                            string? city = Console.ReadLine();
                            Console.Clear();

                            Console.WriteLine("Enter Postal Code:");
                            string? postal = Console.ReadLine();
                            Console.Clear();

                            Console.WriteLine("Enter Street Adress including number:");
                            string? street = Console.ReadLine();
                            Console.Clear();

                            int cusId = CreateCustomer(name, city, postal, street);

                            Console.WriteLine($"Name: {name}\nCity: {city}\nPostal Code: {postal}\nStreet Address: {street}");
                            Thread.Sleep(3000);
                            Console.Clear();

                            while (choice)
                            {
                                Console.WriteLine("How would you like it shipped?\n\n");
                                Console.WriteLine("1 - Mailbox - Standard 3 day Delivery | FREE");
                                Console.WriteLine("2 - Early Bird - Quick 1 day Delivery | 100SEK");

                                ConsoleKeyInfo shippingKey = Console.ReadKey(true);

                                Console.Clear();

                                if (char.IsDigit(shippingKey.KeyChar))                              // Checks whether input is a number or not
                                {
                                    shippingMethod = int.Parse(shippingKey.KeyChar.ToString());

                                    if (shippingMethod == 1 || shippingMethod == 2)
                                    {
                                        Payment(shippingMethod, cusId);
                                        choice = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not an option.");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input. Please select 1 or 2.");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                            }
                            break;
                    }
                }
            }
        }

        public static int CreateCustomer(string name, string city, string postal, string street)
        {
            using (var db = new Models.MyDbContext())
            {
                var customer = new Customer
                {
                    Name = name,
                    City = city,
                    Postal = postal,
                    Street = street,
                };
                db.Customers.Add(customer);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }

                return customer.Id;
            }
        }

        public static void Payment(int shipping, int cusId)                                     
        {
            bool quit = false;
            int paymentMethod;

            using (var db = new Models.MyDbContext())
            {
                while (!quit && !Program.exit)
                {
                    bool pay = true;

                    Console.WriteLine("Payment Details for your order\n");
                    Console.WriteLine("Press Enter to continue");
                    Console.WriteLine("Q - Go back to the Shopping Cart");
                    Console.WriteLine("ESC - Exits the Webshop");

                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.Q:
                            quit = true;
                            break;

                        case ConsoleKey.Escape:
                            quit = true;
                            Program.exit = true;
                            break;

                        default:

                            Console.Clear();

                            while (pay)
                            {
                                if (shipping == 1)
                                {
                                    Console.WriteLine("Standard Shipping\n");
                                }
                                else if (shipping == 2)
                                {
                                    Console.WriteLine("Early Bird (100SEK)\n");
                                    ShoppingCart.sum += 100;
                                }

                                Console.WriteLine($"You currently have these items with a total sum of {ShoppingCart.sum}kr\n");    // 13: Produkterna visas med pris // 14: Pris med frakt samt moms visas 

                                for (int i = 0; i < Program.itemList.Count; i++)
                                {
                                    if (Program.itemList[i].Quantity > 0)
                                    {
                                        Console.WriteLine($"{Program.itemList[i].Id}: {Program.itemList[i].Name} ({Program.itemList[i].Quantity})");

                                        var item = db.Items.FirstOrDefault(c => c.Id == Program.itemList[i].Id);

                                        item.Inventory = Program.itemList[i].Inventory;
                                    }
                                }

                                Console.WriteLine("\nHow would you like to pay?\n\n");
                                Console.WriteLine("1 - Card Payment");
                                Console.WriteLine("2 - Invoice to your address");

                                ConsoleKeyInfo paymentKey = Console.ReadKey(true);

                                Console.Clear();

                                if (char.IsDigit(paymentKey.KeyChar))
                                {
                                    paymentMethod = int.Parse(paymentKey.KeyChar.ToString());

                                    if (paymentMethod == 1)                                               // 15: Minst två val av betalningsmetod
                                    {
                                        Console.WriteLine("Enter your Cardholder Name:");                                      // 11: Namn och adress ska gå att fylla i
                                        string? cardName = Console.ReadLine();
                                        Console.Clear();

                                        Console.WriteLine("Enter your Card Number:");
                                        string? cardNumber = Console.ReadLine();
                                        Console.Clear();

                                        Console.WriteLine("Enter the Expiry Date:");
                                        string? expiryDate = Console.ReadLine();
                                        Console.Clear();

                                        Console.WriteLine("Enter the CVC (3 numbers on the back of the card):");
                                        string? cvc = Console.ReadLine();
                                        Console.Clear();

                                        var newCardPaymentInfo = new Models.CardPaymentInfo
                                        {
                                            CardName = cardName,
                                            CardNumber = cardNumber,
                                            ExpiryDate = expiryDate,
                                            CVC = cvc,
                                            CustomerId = cusId,
                                        };

                                        db.CardPaymentInfo.Add(newCardPaymentInfo);

                                        try
                                        {
                                            db.SaveChanges();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.InnerException.Message);
                                        }

                                        PaymentDone(shipping, paymentMethod, cusId);
                                        pay = false;
                                    }
                                    else if (paymentMethod == 2)
                                    {
                                        try
                                        {
                                            db.SaveChanges();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.InnerException.Message);
                                        }

                                        PaymentDone(shipping, paymentMethod, cusId);
                                        pay = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not an option.");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input. Please select 1 or 2.");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                            }
                            break;
                    }
                }
            }
        }

        public static void PaymentDone(int shipping, int payment, int cusId)                   // 16: Varukorgen ska tömmas här och kunden + kundinfo ska sparas, även hela beställningen sparas
        {
            using (var db = new Models.MyDbContext())
            {
                Console.Clear();

                int orderNumber = Random.Shared.Next(10000, 100000);

                var customer = db.Customers.FirstOrDefault(v => v.Id == cusId);

                var order = new PreviousOrder
                {
                    OrderNum = orderNumber,
                    CustomerId = customer.Id,
                    Customer = customer,
                };

                db.PreviousOrders.Add(order);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }

                Console.WriteLine("Thank you for your purchase!\n");
                Console.WriteLine($"Your Order Number is {orderNumber}\n");

                switch (shipping, payment)
                {
                    case (1, 1):
                        Console.WriteLine($"{ShoppingCart.sum}SEK has been withdrawn from your card and the order will arrive to your address in a few days!");
                        break;

                    case (2, 1):
                        Console.WriteLine($"{ShoppingCart.sum}SEK has been withdrawn from your card and the order will arrive to your address tomorrow!");
                        break;

                    case (1, 2):
                        Console.WriteLine($"An invoice of {ShoppingCart.sum}SEK will be sent along with your order in a few days!");
                        break;

                    case (2, 2):
                        Console.WriteLine($"An invoice of {ShoppingCart.sum}SEK will be sent along with your order tomorrow!");
                        break;
                };

                Console.WriteLine("\nPress any key to exit the webshop");
                Console.ReadKey(true);

                Console.Clear();
                Program.exit = true;
            }
        }
    }
}