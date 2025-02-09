// Code snippet to create an Inventory Management System in C#.
class Product(string name, decimal price, int stockQuantity)
{
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public int StockQuantity { get; set; } = stockQuantity;
}

class InventoryManagement
{
    // Main method to run the program.
    static void Main(string[] args)
    {
        // Create a list to store the products.
        List<Product> products = [];

        // Display the welcome message.
        WriteColoredLine("***Welcome to Inventory Management System.***", ConsoleColor.White);

        // Check if there are any products in the inventory.
        CheckIfAnyProducts(products);

        // Run the program until the user exits.
        while(true)
        {
            // Print the product details.
            PrintAllProductsDetails(products);

            // Print the menu options.
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1. Add a new product");
            Console.WriteLine("2. Sold products");
            Console.WriteLine("3. Restock products");
            Console.WriteLine("4. Delete a product");
            Console.WriteLine("5. Exit");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Enter your choice: ");

            // Get the user's choice.
            if(int.TryParse(ReadColoredLine(ConsoleColor.Cyan), out int choice))
            {
                switch(choice)
                {
                    // Call the respective method based on the user's choice.
                    case 1:
                        AddProduct(products);
                        break;
                    case 2:
                        SoldProduct(products);
                        break;
                    case 3:
                        RestockProduct(products);
                        break;
                    case 4:
                        DeleteProduct(products);
                        break;
                    case 5:
                        WriteColoredLine("Exiting the system...", ConsoleColor.DarkYellow);
                        return;
                    default:
                        WriteColoredLine("Invalid choice. Please try again.", ConsoleColor.Red);
                        break;
                }
            }
            // If the user enters an invalid choice.
            else
            {
                WriteColoredLine("Invalid choice. Please try again.", ConsoleColor.Red);
            }
        }
    }

    #region [Management Methods]

    // Check if there are any products in the inventory.
    public static bool CheckIfAnyProducts(List<Product> products)
    {
        if(products.Count != 0)
        {
            return true;
        }
        WriteColoredLine("There is no product information in stock. Please add a new product first.\n", ConsoleColor.DarkYellow);
        return false;
    }

    // Print the name, price and stock quantity of all products.
    public static void PrintAllProductsDetails(List<Product> products)
    {
        WriteColoredLine("\n|----------Products Details----------|\n", ConsoleColor.DarkYellow);
        foreach(Product product in products)
        {
            WriteColoredLine( $"Name: {product.Name} | Price: {product.Price} | Stock Quantity: {product.StockQuantity}", ConsoleColor.DarkYellow);
        }
        WriteColoredLine("\n|------------------------------------|\n", ConsoleColor.DarkYellow);
    }

    // Check if the product already exists in the inventory.
    public static bool IsProductExists(List<Product> products, string name)
    {
        return products.Any(product => product.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
    }

    // [Menu] Add a new product to the inventory.
    public static void AddProduct(List<Product> products)
    {
        // Get the product name from the user.
        Console.WriteLine("Enter the name of the product: ");
        string? name;
        while(true)
        {
            name = ReadColoredLine(ConsoleColor.Blue);

            // Check if the name is empty or already exists.
            if(string.IsNullOrWhiteSpace(name)){
                 WriteColoredLine("Invalid name. Please enter a valid name:", ConsoleColor.Red);
                 continue;
            }
            else if(IsProductExists(products, name)){
                WriteColoredLine("Product already exists. Please enter a valid name:", ConsoleColor.Red);
                continue;
            }
            break;
        }

        // Get the price of the product from the user.
        Console.WriteLine("Enter the price of the product: ");
        decimal price;
        while(!Decimal.TryParse(ReadColoredLine(ConsoleColor.Blue), out price) || price <= 0)
        {
            WriteColoredLine("Invalid price. Please enter a valid price:", ConsoleColor.Red);
        }

        // Get the stock quantity of the product from the user.
        Console.WriteLine("Enter the stock quantity of the product: ");
        int stockQuantity;
        while(!int.TryParse(ReadColoredLine(ConsoleColor.Blue), out stockQuantity) || stockQuantity < 0)
        {
            WriteColoredLine("Invalid stock quantity. Please enter a valid stock quantity:", ConsoleColor.Red);
        }

        // Create a new product and add it to the list.
        Product product = new(name, price, stockQuantity);
        products.Add(product);

        WriteColoredLine("Product added successfully.", ConsoleColor.Green);
    }

    // [Menu] Sell a product from the inventory.
    public static void SoldProduct(List<Product> products)
    {
        // Check if there are any products in the inventory.
        if(CheckIfAnyProducts(products))
        {
            // Get the name of the product from the user.
            Console.WriteLine("Enter the name of the product you want to sell: ");
            string? name;
            while(string.IsNullOrWhiteSpace(name = ReadColoredLine(ConsoleColor.Blue)))
            {
                WriteColoredLine("Invalid name. Please enter a valid name:", ConsoleColor.Red);
            }

            // Get the quantity of the product from the user.
            Console.WriteLine("Enter the quantity of the product you want to sell: ");
            int quantity;
            while(!int.TryParse(ReadColoredLine(ConsoleColor.Blue), out quantity) || quantity < 0)
            {
                WriteColoredLine("Invalid quantity. Please enter a valid quantity:", ConsoleColor.Red);
            }

            // Check if the product exists and has sufficient stock quantity.
            foreach(Product product in products)
            {
                if(product.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    // If the stock quantity is sufficient, update the stock quantity.
                    if(product.StockQuantity >= quantity)
                    {
                        product.StockQuantity -= quantity;
                        WriteColoredLine("Product sold successfully.", ConsoleColor.Green);
                    }
                    // If the stock quantity is insufficient, display an error message.
                    else
                    {
                        WriteColoredLine("Insufficient stock quantity.", ConsoleColor.Red);
                    }
                    return;
                }
            }
            // If the product is not found, display an error message.
            WriteColoredLine("Product not found.", ConsoleColor.Red);
        }
    }

    // [Menu] Restock a product in the inventory.
    public static void RestockProduct(List<Product> products)
    {
        // Check if there are any products in the inventory.
        if(CheckIfAnyProducts(products))
        {
            // Get the name of the product from the user.
            Console.WriteLine("Enter the name of the product you want to restock: ");
            string? name;
            while(string.IsNullOrWhiteSpace(name = ReadColoredLine(ConsoleColor.Blue)))
            {
                WriteColoredLine("Invalid name. Please enter a valid name:", ConsoleColor.Red);
            }

            // Get the quantity of the product from the user.
            Console.WriteLine("Enter the quantity of the product you want to restock: ");
            int quantity;
            while(!int.TryParse(ReadColoredLine(ConsoleColor.Blue), out quantity) || quantity < 0)
            {
                WriteColoredLine("Invalid quantity. Please enter a valid quantity:", ConsoleColor.Red);
            }

            // Check if the product exists and restock it.
            foreach(Product product in products)
            {
                // If the product is found, display a success message.
                if(product.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    product.StockQuantity += quantity;
                    WriteColoredLine("Product restocked successfully.", ConsoleColor.Green);
                    return;
                }
            }
            // If the product is not found, display an error message.
            WriteColoredLine("Product not found.", ConsoleColor.Red);
        }
    }

    // [Menu] Delete a product from the inventory.
    public static void DeleteProduct(List<Product> products)
    {
        // Check if there are any products in the inventory.
        if(CheckIfAnyProducts(products))
        {
            // Get the name of the product from the user.
            Console.WriteLine("Enter the name of the product you want to delete: ");
            string? name;
            while(string.IsNullOrWhiteSpace(name = ReadColoredLine(ConsoleColor.Blue)))
            {
                WriteColoredLine("Invalid name. Please enter a valid name:", ConsoleColor.Red);
            }

            // Check if the product exists and delete it.
            foreach(Product product in products)
            {
                // If the product is found, display a success message.
                if(product.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    products.Remove(product);
                    WriteColoredLine("Product deleted successfully.", ConsoleColor.Green);
                    return;
                }
            }
            // If the product is not found, display an error message.
            WriteColoredLine("Product not found.", ConsoleColor.Red);
        }
    }

    #endregion [Management Methods]

    #region [Color Print Methods]

    // Read a line in the specified color.
    static string? ReadColoredLine(ConsoleColor color)
    {
        Console.ForegroundColor = color;
        string? userInput = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        return userInput;
    }

    // Write a message in the specified color.
    static void WriteColoredLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    #endregion [Color Print Methods]
}