using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

namespace ShoppingApp.Controllers;

public class WishlistController(
    IWishlistService wishlistService,
    ICartService cartService,
    IOrderService orderService,
    IInvoiceService invoiceService,
    IInvoicePdfService invoicePdfService)
{
    public void Start(User user)
    {
        while (true)
        {
            IO.ShowHeader("MY WISHLIST");

            List<Product> products =
                wishlistService.GetWishlist(
                    user.UserId);

            if (!products.Any())
            {
                IO.PrintInvalidMessage(
                    "Wishlist is empty.");

                return;
            }

            ProductView.DisplayProducts(products);

            string selectedProduct =
                IO.ShowSelectionMenu(
                    "Select Product",
                    products
                        .Select(product => product.Name)
                        .Append("Back")
                        .ToArray(), 10);

            if (selectedProduct == "Back")
            {
                return;
            }

            Product product =
                products.First(
                    p => p.Name == selectedProduct);

            this.ShowProductActions(
                user,
                product);
        }
    }

    private void ShowProductActions(
        User user,
        Product product)
    {
        string choice;
        if (product.Stock == 0)
        {
            choice =
            IO.ShowSelectionMenu(
                "Choose Action",
                [
                    "Add To Cart",
                    "Remove From Wishlist",
                    "Back"
                ]);
        }
        else
        {
            choice =
                IO.ShowSelectionMenu(
                    "Choose Action",
                    [
                        "Add To Cart",
                    "Buy Now",
                    "Remove From Wishlist",
                    "Back"
                    ], 4);
        }

        switch (choice)
        {
            case "Add To Cart":
                this.AddToCart(
                    user,
                    product);
                break;

            case "Buy Now":
                this.BuyNow(
                    user,
                    product);
                break;

            case "Remove From Wishlist":
                this.RemoveFromWishlist(
                    user,
                    product);
                break;

            case "Back":
                return;
        }
    }

    private void AddToCart(
    User user,
    Product product)
    {
        try
        {
            Cart cart =
                cartService.GetCart(
                    user.UserId);

            CartItem? cartItem =
                cart.Items.FirstOrDefault(
                    item => item.ProductId == product.ProductId);

            string prompt =
                cartItem == null
                    ? $"Enter quantity (Max {product.Stock})"
                    : $"Enter quantity (Max {product.Stock}, Already in Cart: {cartItem.Quantity})";

            int quantity =
                IO.GetIntInput(prompt);

            cartService.AddToCart(
                user.UserId,
                product.ProductId,
                quantity);

            IO.PrintSuccessMessage(
                cartItem == null
                    ? "Product added to cart."
                    : $"Product quantity updated. Total in cart: {cartItem.Quantity + quantity}");
        }
        catch (Exception exception)
        {
            IO.PrintInvalidMessage(
                exception.Message);
        }
    }

    private void BuyNow(
        User user,
        Product product)
    {
        try
        {
            int quantity =
                    IO.GetIntInput(
                        $"Enter quantity (Max {product.Stock})");

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
                invoicePdfService
                    .GenerateInvoicePdf(
                        invoice);

            IO.PrintSuccessMessage(
                $"Order placed successfully.\nInvoice saved at:\n{pdfPath}");
        }
        catch (Exception exception)
        {
            IO.PrintInvalidMessage(
                exception.Message);
        }
    }

    private void RemoveFromWishlist(
        User user,
        Product product)
    {
        wishlistService.RemoveFromWishlist(
            user.UserId,
            product.ProductId);

        IO.PrintSuccessMessage(
            "Removed from wishlist.");
    }
}