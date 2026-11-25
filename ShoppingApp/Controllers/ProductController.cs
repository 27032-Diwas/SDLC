using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

namespace ShoppingApp.Controllers;

public class ProductController(
    User user,
    IProductService productService,
    ICartService cartService,
    IWishlistService wishlistService)
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
        string category =
            IO.GetStringInput(
                "[cyan]Enter category:[/]");

        List<Product> products =
            productService.SearchByCategory(category);

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
                    .ToArray());

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

    private void ShowProductDetails(
        Product product)
    {
        while (true)
        {
            IO.ShowHeader(
                product.Name);

            IO.PrintInConsole(
                $"[cyan]Description:[/] {product.Description}");

            IO.PrintInConsole(
                $"[cyan]Category:[/] [green]{product.Category}[/]");

            IO.PrintInConsole(
                $"[cyan]Price:[/] [green]₹{product.Price}[/]");

            IO.PrintInConsole(
                $"[cyan]Stock:[/] [green]{product.Stock}[/]");

            string choice =
                IO.ShowSelectionMenu(
                    "[bold cyan]Choose an action:[/]",
                    [
                        "Add To Cart",
                        "Add To Wishlist",
                        "Back"
                    ]);

            switch (choice)
            {
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

    private void AddToCart(
        Product product)
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