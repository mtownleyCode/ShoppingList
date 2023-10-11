using ShoppingList.Models;
using System.Collections;
using System.Globalization;

namespace ShoppingList.UserInput
{
    public class CustomerInput
    {
        public void GetItems(Customer customer, bool firstLoop, Store store)
        {
            int iCNT = 1;
            int convertedNumber;
            
            string? validInput;

            double priceAdd;

            bool redoLoop = true;
            
            Console.WriteLine();

            if (firstLoop)
            {
                Console.WriteLine("Here is a list of our inventory from lowest to highest price:");

                Console.WriteLine();

                Console.WriteLine(String.Format("{0,-10} {1,-20} {2, -10}", "Item#", "Name", "Price"));

                Console.WriteLine(String.Format("{0,-10} {1,-20} {2, -10}", "==========",
                                                                            "====================",
                                                                            "=========="));

                foreach (KeyValuePair<string, double> kvp in store.inventory.OrderBy(key => key.Value))
                {
                    Item newItem = new Item();

                    newItem.id = iCNT;
                    newItem.name = kvp.Key;
                    newItem.price = kvp.Value;

                    store.itemsToPrices.Add(newItem);

                    Console.WriteLine(String.Format("{0,-10} {1,-20} {2, 10}", iCNT, kvp.Key, kvp.Value.ToString("C", CultureInfo.CurrentCulture)));
                    
                    iCNT++; 
                }

                Console.WriteLine();
            }

            while (redoLoop)
            {
                if (firstLoop)
                {
                    Console.WriteLine("Enter an item id or item name to purchase:");
                }
                else Console.WriteLine("Enter another item id or item name to purchase:");

                validInput = Console.ReadLine();
                
                if ((!int.TryParse(validInput, out convertedNumber) ||
                     convertedNumber < 1 ||
                     convertedNumber > store.inventory.Count) &&
                     string.IsNullOrEmpty(validInput) ||
                    (convertedNumber == 0 &&
                     !store.inventory.ContainsKey(validInput)))
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter a valid item.");
                    Console.WriteLine();
                    redoLoop = true;

                }
                else
                {
                    if (convertedNumber == 0)
                    {
                        var itemToAdd = store.itemsToPrices.FirstOrDefault(i => i.name.ToLower() == validInput.ToLower());
                        
                        customer.purchasedItems.Add(itemToAdd);                        

                    }
                    else
                    {
                        var itemToAdd = store.itemsToPrices.FirstOrDefault(i => i.id == Convert.ToInt32(validInput));

                        customer.purchasedItems.Add(itemToAdd);

                    }

                    redoLoop = false;
                    Console.WriteLine();

                }

            }

        }

        public bool ContinueShopping(Customer customer)
        {
            char validAnswer;

            double priceAdd = 0;

            bool redoLoop = true;
            bool continueShopping = false;

            while (redoLoop)
            {
                Console.WriteLine("Do you want to continue shopping? y/n");

                if (!char.TryParse(Console.ReadLine().ToLower(), out validAnswer) ||
                    validAnswer.CompareTo('y') != 0 &&
                    validAnswer.CompareTo('n') != 0)
                {
                    Console.WriteLine();

                    Console.WriteLine("Enter y or n only.");

                    Console.WriteLine();

                    redoLoop = true;
                }

                else
                {
                    if (validAnswer.CompareTo('y') == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You chose to continue shopping.");
                        continueShopping = true;
                    }
                    else
                    {
                        Console.WriteLine();

                        Console.WriteLine("Here is a little information about your order...");

                        Console.WriteLine();

                        var maxItem = customer.purchasedItems.FirstOrDefault(i=> i.price == customer.purchasedItems.Max(p => p.price));

                        Console.WriteLine($"Your highest priced item is { maxItem.name } at { maxItem.price.ToString("C", CultureInfo.CurrentCulture) } ");

                        var minItem = customer.purchasedItems.FirstOrDefault(i => i.price == customer.purchasedItems.Min(p => p.price));
                        
                        Console.WriteLine($"Your lowest priced item is { minItem.name } at { minItem.price.ToString("C", CultureInfo.CurrentCulture) } ");

                        Console.WriteLine();

                        Console.WriteLine($"You have purchased these items in order by price:");

                        Console.WriteLine();

                        var purchasedItemsSorted = customer.purchasedItems.OrderBy(p => p.price).ToList();

                        Console.WriteLine(String.Format("{0,-20} {1,-10}", "Item", "Price"));
                        Console.WriteLine(String.Format("{0,-10} {1, -10}", "====================", "=========="));

                        foreach (var purchasedItem in purchasedItemsSorted)
                        {
                            Console.WriteLine(String.Format("{0,-20} {1, -10}", purchasedItem.name, purchasedItem.price.ToString("C", CultureInfo.CurrentCulture)));
                        }

                        Console.WriteLine();

                        Console.WriteLine($"Your total is: {customer.purchasedItems.Sum(p=>p.price).ToString("C", CultureInfo.CurrentCulture)}");

                        Console.WriteLine();

                        Console.WriteLine("Thank you for shopping at Freddy's Fine Foods. Goodbye.");
                        continueShopping = false;
                    }

                    redoLoop = false;
                }

            }

            return continueShopping;

        }
    }
}
