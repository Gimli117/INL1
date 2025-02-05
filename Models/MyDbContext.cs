using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MenuTemplateForINL1.Models
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Item> INL1Items { get; set; }                          // Table for all items
        public DbSet<Customer> INL1Customers { get; set; }                  // All customers that have placed orders in the webshop
        public DbSet<PreviousOrder> INL1PreviousOrders { get; set; }        // All completed orders which should include Order ID, which customer who made the purchase as well as purchased items
        public DbSet<CardPaymentInfo> INL1CardPaymentInfo { get; set; }     // Payment info for each customer
        public DbSet<Category> INL1Categories { get; set; }                 // All categories

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)       // Entity Framework, AZURE FreeDB, server gimlidb
        {
            optionsBuilder.UseSqlServer("Server = tcp:gimlidb.database.windows.net, 1433; Initial Catalog = FreeDB; Persist Security Info = False; User ID = gimli117;" +
                "Password =DrunkDwarf117; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = True;", options => options.EnableRetryOnFailure());
        }     
    }
}