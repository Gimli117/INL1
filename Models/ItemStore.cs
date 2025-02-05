using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuTemplateForINL1.Models
{
    internal class ItemStore
    {
        private static List<Item> _items = new();

        public static List<Item> GetItems()
        {
            return _items;
        }

        public static void SetItems (List<Item> items)
        {
            _items = items;
        }

        public static void AddItem (Item item)
        {
            _items.Add(item);
        }
    }
}
