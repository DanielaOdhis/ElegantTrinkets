using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElegantTrinkets
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Elegant Trinkets Beauty Store ;)!!");

            List<Products<string>> products = new List<Products<string>>()
            {
                new Products<string>{Id=1, Name="Face Mask", Description="Peel-Off Mask", Price=350.00, Quantity=10, AdditionalInfo="Whitening, Purifying and Rejuvenation System"},
                new Products<string>{Id=2, Name="Nice & Lovely Body Lotion", Description="Revitalizing carrot oil and vitamin E", Price=120.00, Quantity=13, AdditionalInfo="180 ml bottle"},
                new Products<string>{Id=3, Name="Moisturizing Spray", Description="Suitable for dreadlocks, natural hair, braids & cornrows", Price=160.00, Quantity=2, AdditionalInfo="250 ml bottle" },
            };

            ShoppingCart<Products<string>> cart = new ShoppingCart<Products<string>>();
            bool continueShopping = true;

            while (continueShopping)
            {
                // Display the main  menu
                Console.WriteLine("\nHow will we help you today?");
                Console.WriteLine("1. View cart");
                Console.WriteLine("2. View products");
                Console.WriteLine("3. Checkout and Exit");

                string nextAction = Console.ReadLine();

                switch (nextAction)
                {
                    case "1":
                        // View cart
                        Console.WriteLine("Cart Items:");
                        foreach (var item in cart.GetItems())
                        {
                            Console.WriteLine($"{item.product.Name} - ksh{item.product.Price} ({item.quantityAdded} units) ({item.product.AdditionalInfo})");
                        }
                        Console.WriteLine("Total: ksh " + cart.GetPrice());
                        break;

                    case "2":
                        // View products and allow add/remove
                        Console.WriteLine("\nSelect a product: ");
                        for (int i = 0; i < products.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {products[i].Name} - ksh{products[i].Price}");
                        }

                        int chosenProductIndex = int.Parse(Console.ReadLine()) - 1;
                        Products<string> chosenProduct = products[chosenProductIndex];

                        Console.WriteLine("Do you want to add or remove product? (add/remove)");
                        string choice = Console.ReadLine().ToLower();

                        if (choice == "add")
                        {
                            Console.WriteLine("How many units would you like to add?");
                            int quantityToAdd = int.Parse(Console.ReadLine());
                            await cart.AddProductAsync(chosenProduct, quantityToAdd);
                            Console.WriteLine("Successfully added to cart :)");
                        }
                        else if (choice == "remove")
                        {
                            Console.WriteLine("How many units would you like to remove?");
                            int quantityToRemove = int.Parse(Console.ReadLine());
                            await cart.RemoveProductAsync(chosenProduct, quantityToRemove);
                            Console.WriteLine("Successfully removed from cart :)");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice");
                        }
                        break;

                    case "3":
                        // Checkout and Exit
                        Console.WriteLine("Checking out...");
                        Console.WriteLine("Cart Items:");
                        foreach (var item in cart.GetItems())
                        {
                            Console.WriteLine($"{item.product.Name} - ksh{item.product.Price} ({item.quantityAdded} units) ({item.product.AdditionalInfo})");
                        }
                        Console.WriteLine("Total: ksh " + cart.GetPrice());
                        Console.WriteLine("Thank you for shopping at Elegant Trinkets Beauty Store!");
                        continueShopping = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option, please choose again.");
                        break;
                }
            }
        }
    }
}
