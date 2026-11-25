using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

namespace ShoppingApp.Controllers;

public class WishlistController(
    IWishlistService wishlistService)
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

            string choice =
                IO.ShowSelectionMenu(
                    "Choose an option",
                    [
                        "Remove Product",
                        "Back"
                    ]);

            switch (choice)
            {
                case "Remove Product":
                    this.RemoveFromWishlist(
                        user,
                        products);
                    break;

                case "Back":
                    return;
            }
        }
    }

    private void RemoveFromWishlist(
        User user,
        List<Product> products)
    {
        string selectedProduct =
            IO.ShowSelectionMenu(
                "Select Product",
                products
                    .Select(product => product.Name)
                    .ToArray());

        Product product =
            products.First(
                x => x.Name == selectedProduct);

        wishlistService.RemoveFromWishlist(
            user.UserId,
            product.ProductId);

        IO.PrintSuccessMessage(
            "Removed from wishlist.");
    }
}