using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

namespace ShoppingApp.Controllers;

public class ProductController(
    User user,
    IProductService productService,
    ICartService cartService,
    IWishlistService wishlistService,
    IOrderService orderService,
    IInvoiceService invoiceService,
    IInvoicePdfService invoicePdfService)
{
    public void ShowAllProducts()
    {
        List<Product> products =
            productService.GetAllProducts();

        this.DisplayProducts(products);
    }

    public void SearchByName()
    {
        string keyword =
            IO.GetStringInput(
                "[cyan]Enter product name:[/]");

        List<Product> products =
            productService.SearchByName(keyword);

        this.DisplayProducts(products);
    }

    public void SearchByCategory()
    {
        string[] categories =
        productService
            .GetCategories()
            .Append("Back")
            .ToArray();

        string selectedCategory =
            IO.ShowSelectionMenu(
                "[cyan]Select Category:[/]",
                categories, 10);

        if (selectedCategory == "Back")
        {
            return;
        }

        List<Product> products =
            productService.SearchByCategory(selectedCategory);

        this.DisplayProducts(products);
    }

    public void SearchByPriceRange()
    {
        decimal minPrice =
            decimal.Parse(
                IO.GetStringInput(
                    "[cyan]Enter minimum price:[/]"));

        decimal maxPrice =
            decimal.Parse(
                IO.GetStringInput(
                    "[cyan]Enter maximum price:[/]"));

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
                    "[bold cyan]Select a product:[/]",
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
        string choice;
        while (true)
        {
            IO.ShowHeader(
                product.Name);

            IO.PrintInConsole(
                $"[cyan]Description:[/] {product.Description}");

            IO.PrintInConsole(
                $"[cyan]Category:[/] [green]{product.Category}[/]");

            IO.PrintInConsole(
                $"[cyan]Price:[/] [green]Rs.{product.Price}[/]");
            if (product.Stock == 0)
            {
                IO.PrintInConsole(
                $"[cyan]Stock:[/] [red] Out of stock [/]");

                choice =
                    IO.ShowSelectionMenu(
                        "[bold cyan]Choose an action:[/]",
                        [
                        "Add To Wishlist",
                        "Back"
                        ]);
            }
            else
            {
                IO.PrintInConsole(
                    $"[cyan]Stock:[/] [green]{product.Stock}[/]");

                choice =
                    IO.ShowSelectionMenu(
                        "[bold cyan]Choose an action:[/]",
                        [
                            "Buy Now",
                            "Add To Cart",
                        "Add To Wishlist",
                        "Back"
                        ], 4);
            }

            switch (choice)
            {
                case "Buy Now":
                    this.BuyNow(product);
                    break;
                case "Add To Cart":
                    this.AddToCart(product);
                    break;

                case "Add To Wishlist":
                    this.AddToWishlist(product);
                    break;

                case "Back":
                    return;
            }
        }
    }

    private void BuyNow(Product product)
    {
        try
        {
            int quantity =
                int.Parse(
                    IO.GetStringInput(
                        $"Enter quantity (Max {product.Stock})"));

            if (quantity <= 0)
            {
                IO.PrintInvalidMessage(
                    "Quantity should be greater than 0.");

                return;
            }

            if (quantity > product.Stock)
            {
                IO.PrintInvalidMessage(
                    "Insufficient stock.");

                return;
            }

            CartItem item = new()
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity
            };

            Order order = new()
            {
                OrderId = Guid.NewGuid(),
                UserId = user.UserId,
                Items = [item],
                OrderDate = DateTime.Now,
                TotalAmount = item.TotalPrice,
                Status = "Placed"
            };

            orderService.PlaceDirectOrder(order);

            Invoice invoice =
                invoiceService.GenerateInvoice(
                    order,
                    user);

            InvoiceView.DisplayInvoice(
                invoice);

            string filePath =
                invoicePdfService.GenerateInvoicePdf(
                    invoice);

            IO.PrintSuccessMessage(
                $"Order placed successfully.\nInvoice saved at:\n{filePath}");
        }
        catch (Exception exception)
        {
            IO.PrintInvalidMessage(
                exception.Message);
        }
    }

    private void AddToCart(
        Product product)
    {
        try
        {
            int quantity =
                int.Parse(
                    IO.GetStringInput(
                        $"Enter quantity (Max: {product.Stock}): "));

            if (quantity <= 0)
            {
                IO.PrintInvalidMessage(
                    "Quantity must be greater than 0.");

                return;
            }

            if (quantity > product.Stock)
            {
                IO.PrintInvalidMessage(
                    "Quantity exceeds available stock.");

                return;
            }

            cartService.AddToCart(
                user.UserId,
                product.ProductId,
                quantity);

            IO.PrintSuccessMessage(
                "Product added to cart.");
        }
        catch (Exception exception)
        {
            IO.PrintInvalidMessage(
                exception.Message);
        }
    }

    private void AddToWishlist(
        Product product)
    {
        try
        {
            wishlistService.AddToWishlist(
                user.UserId,
                product.ProductId);

            IO.PrintSuccessMessage(
                "Product added to wishlist.");
        }
        catch (Exception exception)
        {
            IO.PrintInvalidMessage(
                exception.Message);
        }
    }
}