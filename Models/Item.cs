using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuTemplateForINL1.Models
{
    internal class Item
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string>? Tag { get; set; }
        public int Price { get; set; }                          // In SEK
        public string? Supplier { get; set; }
        [NotMapped]
        public int Quantity { get; set; }                       // Number of items that the customer is currently shopping. Is not available in the Items Table in the Database
        public int Inventory { get; set; }                      // Number of available items in the webshop
        public bool IsSelectedByAdmin { get; set; }             // Admin can choose which items to display on the start page
        public InventoryStatus Status => GetInventoryStatus();  // Stock enum

        private InventoryStatus GetInventoryStatus()
        {
            if (Inventory < 20)
            {
                return InventoryStatus.LowStock;
            }
            if (Inventory == 0)
            {
                return InventoryStatus.OutOfStock;
            }
            return InventoryStatus.InStock;
        }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}