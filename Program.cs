using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_A3_VincentLloyd_Opiana
{
    public class Menu
    {
        public Menu()
        {
            MenuItems = new List<string> { "Coffee", "Tea", "Cake" };
            MenuPrices = new List<decimal> { 2.50m, 1.50m, 3.00m };
        }

        public List<string> MenuItems { get; private set; } = new List<string>();
        public List<decimal> MenuPrices { get; private set; } = new List<decimal>();

        public void AddMenuItem(string itemName, decimal itemPrice)
        {
            MenuItems.Add(itemName);
            MenuPrices.Add(itemPrice);
        }

        public void RemoveMenuItem(int index)
        {
            if (index >= 0 && index < MenuItems.Count)
            {
                MenuItems.RemoveAt(index);
                MenuPrices.RemoveAt(index);
                Console.WriteLine("Item removed successfully!");
            }
            else
            {
                Console.WriteLine("Invalid item index.");
            }
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var menu = new Menu();
            var orderItems = new List<int>();
            var users = new Dictionary<string, string>();
            string? currentUser = null;
            var pos = new PointOfSale();
            decimal discount = 0;

            users.Add("admin", "password");

            Console.WriteLine("Enter username: ");
            string? username = Console.ReadLine()?.Trim();

            Console.WriteLine("Enter password: ");
            string? password = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (users.TryGetValue(username, out string storedPassword) && storedPassword == password)
                {
                    currentUser = username;
                }
                else
                {
                    Console.WriteLine("Invalid login. Exiting...");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Username and password cannot be empty. Exiting...");
                return;
            }

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Coffee Shop!");
                Console.WriteLine("1. Add Menu Item");
                Console.WriteLine("2. Remove Menu Item");
                Console.WriteLine("3. View Menu");
                Console.WriteLine("4. Place Order");
                Console.WriteLine("5. View Order");
                Console.WriteLine("6. Apply Discount");
                Console.WriteLine("7. Calculate Total");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");
                string? choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddMenuItem();
                        break;
                    case "2":
                        RemoveMenuItem();
                        break;
                    case "3":
                        ViewMenu();
                        break;
                    case "4":
                        PlaceOrder();
                        break;
                    case "5":
                        ViewOrder();
                        break;
                    case "6":
                        ApplyDiscount();
                        break;
                    case "7":
                        pos.CalculateTotal(orderItems, menu.MenuPrices, currentUser, discount);
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("Exiting... Press any key to close.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

            void AddMenuItem()
            {
                Console.Write("Enter item name: ");
                string? itemName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(itemName))
                {
                    Console.WriteLine("Item name cannot be empty.");
                    return;
                }

                Console.Write("Enter item price: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal itemPrice))
                {
                    menu.AddMenuItem(itemName, itemPrice);
                    Console.WriteLine("Item added successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid price format.");
                }
            }

            void RemoveMenuItem()
            {
                ViewMenu();
                Console.Write("Enter the item number to remove: ");
                if (int.TryParse(Console.ReadLine(), out int itemNumber))
                {
                    itemNumber--;
                    menu.RemoveMenuItem(itemNumber);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            void ViewMenu()
            {
                Console.Clear();
                Console.WriteLine("Menu:");
                for (int i = 0; i < menu.MenuItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {menu.MenuItems[i]} - {menu.MenuPrices[i]:C}");
                }
            }

            void PlaceOrder()
            {
                ViewMenu();
                Console.Write("Enter the item number to order: ");
                if (int.TryParse(Console.ReadLine(), out int itemNumber))
                {
                    itemNumber--;
                    if (itemNumber >= 0 && itemNumber < menu.MenuItems.Count)
                    {
                        orderItems.Add(itemNumber);
                        Console.WriteLine("Item added to order!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid item number. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            void ViewOrder()
            {
                Console.WriteLine("Your Order:");
                if (orderItems.Count > 0)
                {
                    foreach (int itemIndex in orderItems)
                    {
                        Console.WriteLine($"{menu.MenuItems[itemIndex]} - {menu.MenuPrices[itemIndex]:C}");
                    }
                }
                else
                {
                    Console.WriteLine("No items in your order.");
                }
            }

            void ApplyDiscount()
            {
                Console.Write("Enter discount percentage (e.g., 50 for 50% off): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal discountPercentage) && discountPercentage >= 0 && discountPercentage <= 100)
                {
                    discount = discountPercentage / 100;
                    Console.WriteLine($"Discount of {discountPercentage}% applied!");
                }
                else
                {
                    Console.WriteLine("Invalid discount percentage. Please enter a number between 0 and 100.");
                }
            }
        }
    }

    public class PointOfSale
    {
        public void CalculateTotal(List<int> orderItems, List<decimal> menuPrices, string? currentUser, decimal discount)
        {
            decimal total = 0m;
            foreach (int itemIndex in orderItems)
            {
                if (itemIndex >= 0 && itemIndex < menuPrices.Count)
                {
                    total += menuPrices[itemIndex];
                }
            }

            if (discount > 0)
            {
                total -= total * discount;
            }

            Console.WriteLine($"Total amount for {currentUser}: {total:C}");
        }
    }

    public class Identity
    {
        public Identity(string usersFile)
        {
        }
        public string? Login(string username, string password)
        {
            return username == "admin" && password == "password" ? username : null;
        }
    }
}

using System;
using System.Collections.Generic;

namespace coffee_shop_procedural
{
    public class Menu
    {
        public Menu()
        {
            MenuItems = new List<string> { "Coffee", "Tea", "Cake" };
            MenuPrices = new List<decimal> { 2.50m, 1.50m, 3.00m };
        }

        public List<string> MenuItems { get; private set; } = new List<string>();
        public List<decimal> MenuPrices { get; private set; } = new List<decimal>();

        public void AddMenuItem(string itemName, decimal itemPrice)
        {
            MenuItems.Add(itemName);
            MenuPrices.Add(itemPrice);
        }

        public void RemoveMenuItem(int index)
        {
            if (index >= 0 && index < MenuItems.Count)
            {
                MenuItems.RemoveAt(index);
                MenuPrices.RemoveAt(index);
                Console.WriteLine("Item removed successfully!");
            }
            else
            {
                Console.WriteLine("Invalid item index.");
            }
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var menu = new Menu();
            var orderItems = new List<int>();
            var users = new Dictionary<string, string>();
            string? currentUser = null;
            var pos = new PointOfSale();
            decimal discount = 0;

            users.Add("admin", "password");

            Console.WriteLine("Enter username: ");
            string? username = Console.ReadLine()?.Trim();

            Console.WriteLine("Enter password: ");
            string? password = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (users.TryGetValue(username, out string storedPassword) && storedPassword == password)
                {
                    currentUser = username;
                }
                else
                {
                    Console.WriteLine("Invalid login. Exiting...");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Username and password cannot be empty. Exiting...");
                return;
            }

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Coffee Shop!");
                Console.WriteLine("1. Add Menu Item");
                Console.WriteLine("2. Remove Menu Item");
                Console.WriteLine("3. View Menu");
                Console.WriteLine("4. Place Order");
                Console.WriteLine("5. View Order");
                Console.WriteLine("6. Apply Discount");
                Console.WriteLine("7. Calculate Total");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");
                string? choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddMenuItem();
                        break;
                    case "2":
                        RemoveMenuItem();
                        break;
                    case "3":
                        ViewMenu();
                        break;
                    case "4":
                        PlaceOrder();
                        break;
                    case "5":
                        ViewOrder();
                        break;
                    case "6":
                        ApplyDiscount();
                        break;
                    case "7":
                        pos.CalculateTotal(orderItems, menu.MenuPrices, currentUser, discount);
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("Exiting... Press any key to close.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

            void AddMenuItem()
            {
                Console.Write("Enter item name: ");
                string? itemName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(itemName))
                {
                    Console.WriteLine("Item name cannot be empty.");
                    return;
                }

                Console.Write("Enter item price: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal itemPrice))
                {
                    menu.AddMenuItem(itemName, itemPrice);
                    Console.WriteLine("Item added successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid price format.");
                }
            }

            void RemoveMenuItem()
            {
                ViewMenu();
                Console.Write("Enter the item number to remove: ");
                if (int.TryParse(Console.ReadLine(), out int itemNumber))
                {
                    itemNumber--;
                    menu.RemoveMenuItem(itemNumber);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            void ViewMenu()
            {
                Console.Clear();
                Console.WriteLine("Menu:");
                for (int i = 0; i < menu.MenuItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {menu.MenuItems[i]} - {menu.MenuPrices[i]:C}");
                }
            }

            void PlaceOrder()
            {
                ViewMenu();
                Console.Write("Enter the item number to order: ");
                if (int.TryParse(Console.ReadLine(), out int itemNumber))
                {
                    itemNumber--;
                    if (itemNumber >= 0 && itemNumber < menu.MenuItems.Count)
                    {
                        orderItems.Add(itemNumber);
                        Console.WriteLine("Item added to order!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid item number. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            void ViewOrder()
            {
                Console.WriteLine("Your Order:");
                if (orderItems.Count > 0)
                {
                    foreach (int itemIndex in orderItems)
                    {
                        Console.WriteLine($"{menu.MenuItems[itemIndex]} - {menu.MenuPrices[itemIndex]:C}");
                    }
                }
                else
                {
                    Console.WriteLine("No items in your order.");
                }
            }

            void ApplyDiscount()
            {
                Console.Write("Enter discount percentage (e.g., 50 for 50% off): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal discountPercentage) && discountPercentage >= 0 && discountPercentage <= 100)
                {
                    discount = discountPercentage / 100;
                    Console.WriteLine($"Discount of {discountPercentage}% applied!");
                }
                else
                {
                    Console.WriteLine("Invalid discount percentage. Please enter a number between 0 and 100.");
                }
            }
        }
    }

    public class PointOfSale
    {
        public void CalculateTotal(List<int> orderItems, List<decimal> menuPrices, string? currentUser, decimal discount)
        {
            decimal total = 0m;
            foreach (int itemIndex in orderItems)
            {
                if (itemIndex >= 0 && itemIndex < menuPrices.Count)
                {
                    total += menuPrices[itemIndex];
                }
            }

            if (discount > 0)
            {
                total -= total * discount;
            }

            Console.WriteLine($"Total amount for {currentUser}: {total:C}");
        }
    }

    public class Identity
    {
        public Identity(string usersFile)
        {
        }
        public string? Login(string username, string password)
        {
            return username == "admin" && password == "password" ? username : null;
        }
    }
}
}
