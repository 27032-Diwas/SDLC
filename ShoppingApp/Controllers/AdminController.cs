using ShoppingApp.Constants;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;
using Spectre.Console;

namespace ShoppingApp.Controllers;

public class AdminController(IProductService productService, IOrderService orderService, IAdminService adminService)
{
    public void Start()
    {
        while (true)
        {
            IO.ShowHeader("ADMIN PANEL");

            string choice =
                IO.ShowSelectionMenu(
                    "Select Option",
                    [
                        "View Products",
                        "Search By Name",
                        "Search By Category",
                        "Search By Price Range",
                        "Add Product",
                        "Edit Product",
                        "Delete Product",
                        "View Orders",
                        "View Returned Orders",
                        "Logout",
                        Configurables.Exit
                    ], 11);

            switch (choice)
            {
                case "View Products":
                    this.ViewProducts();
                    break;

                case "Search By Name":
                    this.SearchByName();
                    break;

                case "Search By Category":
                    this.SearchByCategory();
                    break;

                case "Search By Price Range":
                    this.SearchByPriceRange();
                    break;

                case "Add Product":
                    this.AddProduct();
                    break;

                case "Edit Product":
                    this.EditProduct();
                    break;

                case "Delete Product":
                    this.DeleteProduct();
                    break;

                case "View Orders":
                    this.ViewOrders();
                    break;

                case "View Returned Orders":
                    this.ViewReturnedOrders();
                    break;

                case "Logout":
                    return;

                case Configurables.Exit:
                    IO.ExitApplication();
                    return;
            }
        }
    }

    private void ViewProducts()
    {
        IO.ShowHeader("PRODUCTS");
        List<Product> products = productService.GetAllProducts();
        this.DisplayProducts(products);
        IO.WaitInConsole();
    }


    private void SearchByName()
    {
        string keyword = IO.GetStringInput($"[{Site.color}]Enter product name:[/]");

        List<Product> products = productService.SearchByName(keyword);

        this.DisplayProducts(products);
    }

    private void SearchByCategory()
    {
        string[] categories =
        productService
            .GetCategories()
            .Append("Back")
            .ToArray();

        string selectedCategory =
            IO.ShowSelectionMenu(
                $"[{Site.color}]Select Category:[/]",
                categories, 10);

        if (selectedCategory == "Back")
        {
            return;
        }

        List<Product> products =
            productService.SearchByCategory(selectedCategory);

        this.DisplayProducts(products);
    }

    private void SearchByPriceRange()
    {
        decimal minPrice = IO.GetIntInput($"[{Site.color}]Enter minimum price:[/]");

        decimal maxPrice = IO.GetIntInput($"[{Site.color}]Enter maximum price:[/]");

        List<Product> products =
            productService.SearchByPriceRange(
                minPrice,
                maxPrice);

        this.DisplayProducts(products);
    }

    private void DisplayProducts(
        List<Product> products)
    {
        while (true)
        {
            if (!products.Any())
            {
                IO.PrintInvalidMessage(
                    "No products found.");

                return;
            }

            ProductView.DisplayProducts(products);

            string selectedProductName =
                IO.ShowSelectionMenu(
                    $"[{Site.color}]Select a product:[/]",
                    products
                        .Select(product => product.Name)
                        .Append("Back")
                        .ToArray(), 10);

            if (selectedProductName == "Back")
            {
                return;
            }

            Product selectedProduct =
                products.First(
                    product =>
                        product.Name ==
                        selectedProductName);

            this.ShowProductDetails(
                selectedProduct);
        }
    }

    private void ShowProductDetails(
        Product product)
    {
        IO.ShowHeader(
            product.Name);

        IO.PrintInConsole(
            $"[{Site.color}]Description:[/] {product.Description}");

        IO.PrintInConsole(
            $"[{Site.color}]Category:[/] [green]{product.Category}[/]");

        IO.PrintInConsole(
            $"[{Site.color}]Price:[/] [green]Rs.{product.Price}[/]");

        if (product.Stock == 0)
        {
            IO.PrintInConsole(
            $"[{Site.color}]Stock:[/] [red] Out of stock [/]");
        }
        else
        {
            IO.PrintInConsole(
                $"[{Site.color}]Stock:[/] [green]{product.Stock}[/]");
        }

        IO.WaitInConsole();
    }

    private void AddProduct()
    {
        IO.ShowHeader("ADD PRODUCT");

        string name = IO.GetStringInput($"[{Site.color}]Product Name:[/]");

        string description = IO.GetStringInput($"[{Site.color}]Description:[/]");

        string category = IO.GetStringInput($"[{Site.color}]Category:[/]");

        decimal price = IO.GetIntInput($"[{Site.color}]Price:[/]");

        int stock = IO.GetIntInput($"[{Site.color}]Stock:[/]");

        Product product = new()
        {
            ProductId = Guid.NewGuid(),
            Name = name,
            Description = description,
            Category = category,
            Price = price,
            Stock = stock
        };

        productService.AddProduct(product);

        IO.PrintSuccessMessage("Product added successfully.");
    }

    private void EditProduct()
    {
        IO.ShowHeader("EDIT PRODUCT");

        List<Product> products =
            productService.GetAllProducts();

        string selectedProduct =
            IO.ShowSelectionMenu(
                "Select Product",
                products
                    .Select(product => product.Name)
                    .Append("Back")
                    .ToArray(),
                10);

        if (selectedProduct == "Back")
        {
            return;
        }

        Product product =
            products.First(
                product =>
                    product.Name ==
                    selectedProduct);

        while (true)
        {
            IO.ShowHeader($"EDIT : {product.Name}");

            string choice =
                IO.ShowSelectionMenu(
                    "Select Field",
                    [
                        "Name",
                    "Description",
                    "Category",
                    "Price",
                    "Stock",
                    "Save & Back"
                    ], 6);

            switch (choice)
            {
                case "Name":
                    product.Name =
                        IO.GetStringInput(
                            $"Current: {product.Name}");
                    break;

                case "Description":
                    product.Description =
                        IO.GetStringInput(
                            $"Current: {product.Description}");
                    break;

                case "Category":
                    product.Category =
                        IO.GetStringInput(
                            $"Current: {product.Category}");
                    break;

                case "Price":
                    product.Price =
                        IO.GetDecimalInput(
                            $"Current: {product.Price}");
                    break;

                case "Stock":
                    product.Stock =
                        IO.GetIntInput(
                            $"Current: {product.Stock}");
                    break;

                case "Save & Back":

                    adminService.UpdateProduct(
                        product);

                    IO.PrintSuccessMessage(
                        "Product updated successfully.");

                    return;
            }
        }
    }

    private void DeleteProduct()
    {
        IO.ShowHeader("DELETE PRODUCT");

        List<Product> products =
            productService.GetAllProducts();

        string selectedProduct =
            IO.ShowSelectionMenu(
                "Select Product",
                products
                    .Select(product => product.Name)
                    .Append("Back")
                    .ToArray(),
                10);

        if (selectedProduct == "Back")
        {
            return;
        }

        Product product =
            products.First(
                product =>
                    product.Name ==
                    selectedProduct);

        bool confirm =
            IO.ConfirmAction(
                $"Delete {product.Name}?");

        if (!confirm)
        {
            return;
        }

        adminService.DeleteProduct(product.ProductId);

        IO.PrintSuccessMessage(
            "Product deleted successfully.");
    }

    private void ViewOrders()
    {
        IO.ShowHeader("ALL ORDERS");

        List<Order> orders =
            orderService.GetAllOrders();

        if (!orders.Any())
        {
            IO.PrintInvalidMessage(
                "No orders found.");

            return;
        }

        Table table = new();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Order Id");
        table.AddColumn("User Id");
        table.AddColumn("Amount");
        table.AddColumn("Status");
        table.AddColumn("Date");

        foreach (Order order in orders)
        {
            table.AddRow(
                order.OrderId.ToString(),
                order.UserId.ToString(),
                $"Rs. {order.TotalAmount}",
                order.Status,
                order.OrderDate.ToString(
                    "dd-MM-yyyy"));
        }

        AnsiConsole.Write(table);

        IO.WaitInConsole();
    }

    private void ViewReturnedOrders()
    {
        IO.ShowHeader("RETURNED ORDERS");

        List<Order> returnedOrders =
            orderService
                .GetAllOrders()
                .Where(order =>
                    order.Status ==
                    "Returned")
                .ToList();

        if (!returnedOrders.Any())
        {
            IO.PrintInvalidMessage(
                "No returned orders found.");

            return;
        }

        Table table = new();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Order Id");
        table.AddColumn("User Id");
        table.AddColumn("Amount");
        table.AddColumn("Returned Date");

        foreach (Order order in returnedOrders)
        {
            table.AddRow(
                order.OrderId.ToString(),
                order.UserId.ToString(),
                $"Rs. {order.TotalAmount}",
                order.OrderDate.ToString(
                    "dd-MM-yyyy"));
        }

        AnsiConsole.Write(table);

        IO.WaitInConsole();
    }
}