using System;
using System.Linq;
using MarketSatis;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new MarketContext())
        {
            Console.WriteLine("Market Sales Program");
            while (true) 
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Seller");
                Console.WriteLine("2. Add Buyer");
                Console.WriteLine("3. Add User");
                Console.WriteLine("4. Add Product");
                Console.WriteLine("5. Sell Product");
                Console.WriteLine("6. List Products");
                Console.WriteLine("7. Exit");
                Console.Write("Make your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                   case 1:
                        AddSeller(context);
                        break;
                    case 2:
                        AddBuyer(context);
                        break;
                    case 3:
                        AddUser(context);
                        break;
                    case 4:
                        AddProduct(context);
                        break;
                    case 5:
                        SellProduct(context);
                        break;
                    case 6:
                        ListProducts(context);
                        break;
                    case 7:
                        Console.WriteLine("Exiting program. Goodbye!");
                        return; 
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

    static void AddSeller(MarketContext context)
    {
        Console.Write("Seller Name: ");
        string name = Console.ReadLine();
        var seller = new Seller { Name = name };
        context.Sellers.Add(seller);
        context.SaveChanges();
        Console.WriteLine("Seller added.");
    }

    static void AddBuyer(MarketContext context)
    {
        Console.Write("Buyer Name: ");
        string name = Console.ReadLine();
        var buyer = new Buyer { Name = name };
        context.Buyers.Add(buyer);
        context.SaveChanges();
        Console.WriteLine("Buyer added.");
    }

    static void AddUser(MarketContext context)
    {
        Console.Write("User Name: ");
        string name = Console.ReadLine();
        Console.Write("User Type (1 for Seller, 2 for Buyer): ");
        if (int.TryParse(Console.ReadLine(), out int userType) && (userType == 1 || userType == 2))
        {
            if (userType == 1)
                AddSeller(context);
            else if (userType == 2)
                AddBuyer(context);
        }
        else
        {
            Console.WriteLine("Invalid user type. User not added.");
        }
    }

    static void AddProduct(MarketContext context)
    {
        Console.Write("Product Name: ");
        string name = Console.ReadLine();
        Console.Write("Product Price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Seller Name: ");
        string sellerName = Console.ReadLine();

       
        var seller = context.Sellers.FirstOrDefault(s => s.Name == sellerName);
        if (seller == null)
        {
            Console.WriteLine("Seller not found. Please provide a valid Seller Name.");
            return;
        }

   
        var product = new Product { Name = name, Price = price, SellerId = seller.Id };
        context.Products.Add(product);
        context.SaveChanges();
        Console.WriteLine("Product added.");
    }


    static void SellProduct(MarketContext context)
    {
        Console.Write("Buyer Name: ");
        string buyerName = Console.ReadLine();
        Console.Write("Product Name: ");
        string productName = Console.ReadLine();

    
        var buyer = context.Buyers.FirstOrDefault(b => b.Name.Equals(buyerName, StringComparison.OrdinalIgnoreCase));
        if (buyer == null)
        {
            Console.WriteLine("Buyer not found.");
            return;
        }

      
        var product = context.Products
            .Include(p => p.Buyer)
            .FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase) && p.BuyerId == null);

        if (product == null)
        {
            Console.WriteLine("Product not found or already sold.");
            return;
        }

       
        product.BuyerId = buyer.Id;
        context.SaveChanges();
        Console.WriteLine($"Product '{product.Name}' sold to '{buyer.Name}'.");
    }


    static void ListProducts(MarketContext context)
    {
        var products = context.Products.Include(p => p.Seller).Include(p => p.Buyer).ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Seller: {product.Seller?.Name}, Buyer: {product.Buyer?.Name ?? "Not sold yet"}");
        }
    }
}
