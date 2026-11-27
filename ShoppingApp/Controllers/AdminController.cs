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
                        "Import Products From CSV",
                        "Export Products To CSV",
                        "Edit Product",
                        "Delete Product",
                        "View Orders",
                        "View Returned Orders",
                        "Logout"
                    ], 12);

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

                case "Import Products From CSV":
                    this.ImportProducts();
                    break;

                case "Export Products To CSV":
                    this.ExportProducts();
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
        List<Product> products = productService.GetAllProducts();
        this.DisplayProducts(products);
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

    private void ImportProducts()
    {
        IO.ShowHeader("IMPORT PRODUCTS");

        try
        {
            string csvPath =
                IO.GetStringInput(
                    "Enter CSV File Path");

            productService.ImportProducts(
                csvPath);

            IO.PrintSuccessMessage(
                "Products imported successfully.");
        }
        catch (Exception exception)
        {
            IO.PrintInvalidMessage(
                exception.Message);
        }
    }

    private void ExportProducts()
    {
        try
        {
            string filePath =
                productService.ExportProducts(
                    "Data/ProductExport.csv");

            IO.PrintSuccessMessage(
                $"Products exported successfully.\n{filePath}");
        }
        catch (Exception exception)
        {
            IO.PrintInvalidMessage(
                exception.Message);
        }
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
        while (true)
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

            this.DisplayOrders(orders);

            string[] options =
                orders.Select(
                    (order, index) =>
                    {
                        string firstProduct =
                            order.Items.First().ProductName;

                        int remainingProducts =
                            order.Items.Count - 1;

                        return remainingProducts > 0
                            ? $"{index + 1}. {firstProduct} (+{remainingProducts} more)"
                            : $"{index + 1}. {firstProduct}";
                    })
                .Append("Back")
                .ToArray();

            string selectedOption =
                IO.ShowSelectionMenu(
                    "Select Order",
                    options,
                    10);

            if (selectedOption == "Back")
            {
                return;
            }

            int selectedIndex =
                int.Parse(
                    selectedOption
                        .Split('.')[0])
                - 1;

            this.ShowAdminOrderDetails(
                orders[selectedIndex]);
        }
    }

    private void ViewReturnedOrders()
    {
        while (true)
        {
            IO.ShowHeader("RETURNED ORDERS");

            List<Order> returnedOrders =
                orderService
                    .GetAllOrders()
                    .Where(order =>
                        order.Status == "Returned")
                    .ToList();

            if (!returnedOrders.Any())
            {
                IO.PrintInvalidMessage(
                    "No returned orders found.");

                return;
            }

            this.DisplayOrders(returnedOrders);

            string[] options =
                returnedOrders.Select(
                    (order, index) =>
                    {
                        string firstProduct =
                            order.Items.First().ProductName;

                        int remainingProducts =
                            order.Items.Count - 1;

                        return remainingProducts > 0
                            ? $"{index + 1}. {firstProduct} (+{remainingProducts} more)"
                            : $"{index + 1}. {firstProduct}";
                    })
                .Append("Back")
                .ToArray();

            string selectedOption =
                IO.ShowSelectionMenu(
                    "Select Order",
                    options,
                    10);

            if (selectedOption == "Back")
            {
                return;
            }

            int selectedIndex =
                int.Parse(
                    selectedOption
                        .Split('.')[0])
                - 1;

            this.ShowAdminOrderDetails(
                returnedOrders[selectedIndex]);
        }
    }

    private void ShowAdminOrderDetails(
    Order order)
    {
        IO.ShowHeader("ORDER DETAILS");

        IO.PrintInConsole(
            $"[cyan]Order Id:[/] {order.OrderId}");

        IO.PrintInConsole(
            $"[cyan]User Id:[/] {order.UserId}");

        IO.PrintInConsole(
            $"[cyan]Order Date:[/] {order.OrderDate:dd-MM-yyyy HH:mm}");

        IO.PrintInConsole(
            $"[cyan]Status:[/] [green]{order.Status}[/]");

        IO.PrintInConsole(
            $"[cyan]Total Amount:[/] [green]Rs. {order.TotalAmount}[/]");

        IO.PrintEmptyLine();

        Table table = new();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Product");
        table.AddColumn("Quantity");
        table.AddColumn("Unit Price");
        table.AddColumn("Total");

        foreach (CartItem item in order.Items)
        {
            table.AddRow(
                item.ProductName,
                item.Quantity.ToString(),
                $"Rs. {item.UnitPrice}",
                $"Rs. {item.TotalPrice}");
        }

        AnsiConsole.Write(table);

        IO.WaitInConsole();
    }

    private void DisplayOrders(
    List<Order> orders)
    {
        Table table = new();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Products");
        table.AddColumn("Amount");
        table.AddColumn("Status");
        table.AddColumn("Date");

        foreach (Order order in orders)
        {
            string products =
                string.Join(
                    ", ",
                    order.Items.Select(
                        item => item.ProductName));

            table.AddRow(
                products,
                $"Rs. {order.TotalAmount}",
                order.Status,
                order.OrderDate.ToString(
                    "dd-MM-yyyy"));
        }

        AnsiConsole.Write(table);

        IO.PrintEmptyLine();
    }
}