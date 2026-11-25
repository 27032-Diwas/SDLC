using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;
using Spectre.Console;

namespace ShoppingApp.Controllers;

public class AdminController(
    IProductService productService,
    IOrderService orderService)
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
                        "Add Product",
                        "Edit Product",
                        "Delete Product",
                        "View Orders",
                        "View Returned Orders",
                        "Logout"
                    ],7);

            switch (choice)
            {
                case "View Products":
                    this.ViewProducts();
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
            }
        }
    }

    private void ViewProducts()
    {
        IO.ShowHeader("PRODUCTS");

        List<Product> products =
            productService.GetAllProducts();

        ProductView.DisplayProducts(products);

        IO.WaitInConsole();
    }

    private void AddProduct()
    {
        IO.ShowHeader("ADD PRODUCT");

        string name =
            IO.GetStringInput(
                "[cyan]Product Name:[/]");

        string description =
            IO.GetStringInput(
                "[cyan]Description:[/]");

        string category =
            IO.GetStringInput(
                "[cyan]Category:[/]");

        decimal price =
            decimal.Parse(
                IO.GetStringInput(
                    "[cyan]Price:[/]"));

        int stock =
            int.Parse(
                IO.GetStringInput(
                    "[cyan]Stock:[/]"));

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

        IO.PrintSuccessMessage(
            "Product added successfully.");
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

        product.Name =
            IO.GetStringInput(
                $"Product Name ({product.Name})");

        product.Description =
            IO.GetStringInput(
                $"Description ({product.Description})");

        product.Category =
            IO.GetStringInput(
                $"Category ({product.Category})");

        product.Price =
            decimal.Parse(
                IO.GetStringInput(
                    $"Price ({product.Price})"));

        product.Stock =
            int.Parse(
                IO.GetStringInput(
                    $"Stock ({product.Stock})"));

        productService.UpdateProduct(product);

        IO.PrintSuccessMessage(
            "Product updated successfully.");
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

        productService.DeleteProduct(
            product.ProductId);

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