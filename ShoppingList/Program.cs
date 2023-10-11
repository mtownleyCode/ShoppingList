using ShoppingList.Models;
using ShoppingList.UserInput;

bool continueShopping = true;
bool firstLoop = true;

Customer customer = new Customer();
CustomerInput customerInput = new CustomerInput();
Store store = new Store();

Console.WriteLine("Welcome to Freddy's Fine Foods.");

Console.WriteLine();

while (continueShopping)
{   
    customerInput.GetItems(customer, firstLoop, store); 
        
    continueShopping = customerInput.ContinueShopping(customer);

    firstLoop = false;

}