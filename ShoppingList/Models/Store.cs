using System.Collections;

namespace ShoppingList.Models
{
    public class Store
    {
        public SortedDictionary<string, double> inventory = new SortedDictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            {"Chips", 3.49},
            {"Bread", 2.50},
            {"Peanut Butter", 5.58},
            {"Steak", 14.65},
            {"Soda", 3.99},
            {"Flour", 4.65},
            {"Eggs", 3.89},
            {"Soup", 2.99}

        };

        public List<Item> itemsToPrices = new List<Item>();

    }

}
