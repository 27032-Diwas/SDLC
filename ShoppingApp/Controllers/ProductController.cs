using ShoppingApp.Constants;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

namespace ShoppingApp.Controllers;

public class ProductController(
    User user,
    IUserService userService,
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
        string keyword = IO.GetStringInput($"[{Site.color}]Enter product name:[/]");

        List<Product> products = productService.SearchByName(keyword);

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

    public void SearchByPriceRange()
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
        string choice;
        while (true)
        {
            product = productService.GetProductById(product.ProductId)!;

            IO.ShowHeader(
                product.Name);

            IO.PrintInConsole(
                $"[{Site.color}]Description:[/] {product.Description}");

            IO.PrintInConsole(
                $"[{Site.color}]Category:[/] [green]{product.Category}[/]");

            IO.PrintInConsole(
                $"[{Site.color}]Price:[/] [green]Rs.{product.Price}[/]");

            Cart cart = cartService.GetCart(user.UserId);

            CartItem? cartItem = cart.Items.FirstOrDefault(item => item.ProductId == product.ProductId);

            if (cartItem != null)
            {
                IO.PrintInConsole($"[{Site.color}]In Cart:[/] [yellow]{cartItem.Quantity} item(s)[/]");
            }

            if (product.Stock == 0)
            {
                IO.PrintInConsole(
                $"[{Site.color}]Stock:[/] [red] Out of stock [/]");

                choice =
                    IO.ShowSelectionMenu(
                        $"[{Site.color}]Choose an action:[/]",
                        [
                        "Add To Wishlist",
                        "Back"
                        ]);
            }
            else
            {
                IO.PrintInConsole(
                    $"[{Site.color}]Stock:[/] [green]{product.Stock}[/]");

                choice =
                    IO.ShowSelectionMenu(
                        $"[{Site.color}]Choose an action:[/]",
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
            int quantity = IO.GetIntInput($"Enter quantity (Max {product.Stock})");

            if (user.Address == null)
            {
                Address address =
                    this.GetAddress();

                user.Address = address;

                userService.UpdateUser(user);
            }

            bool choice = IO.ConfirmAction("Want to confirm order?");

            if (!choice)
            {
                return;
            }

            Order order =
                orderService.PlaceDirectOrder(
                    user.UserId,
                    product.ProductId,
                    quantity);

            Invoice invoice =
                invoiceService.GenerateInvoice(
                    order,
                    user);

            InvoiceView.DisplayInvoice(
                invoice);

            string pdfPath =
                invoicePdfService.GenerateInvoicePdf(
                    invoice);

            IO.PrintSuccessMessage(
                $"Order placed successfully.\nInvoice saved at:\n{pdfPath}");
        }
        catch (Exception exception)
        {
            IO.PrintInvalidMessage(exception.Message);
        }
    }

    private void AddToCart(
        Product product)
    {
        try
        {
            int quantity =
                    IO.GetIntInput(
                        $"Enter quantity (Max: {product.Stock}): ");

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

    private Address GetAddress()
    {
        string houseNo =
            UserView.GetValidAddress();

        string selectedState =
            IO.ShowSelectionMenu(
                "Select State",
                StateDistrictData
                    .StatesAndDistricts
                    .Keys
                    .ToArray());

        IO.PrintInConsole($"[{Site.color}]State:[/] [green]{selectedState}[/]");

        string selectedDistrict =
            IO.ShowSelectionMenu(
                "Select District",
                StateDistrictData
                    .StatesAndDistricts[selectedState]
                    .ToArray());

        IO.PrintInConsole($"[{Site.color}]District:[/] [green]{selectedDistrict}[/]");

        string pinCode =
            UserView.GetValidatedPincode();

        return new Address(
            houseNo,
            selectedDistrict,
            selectedState,
            pinCode);
    }
}