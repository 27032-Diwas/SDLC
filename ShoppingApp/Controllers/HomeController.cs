using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

namespace ShoppingApp.Controllers;

public class HomeController(
    User user,
    IProductService productService,
    ICartService cartService,
    IOrderService orderService,
    IWishlistService wishlistService,
    IInvoiceService invoiceService)
{
    public void Start()
    {
        while (true)
        {
            IO.ShowHeader(
                $"WELCOME {user.Name.ToUpper()}");

            string choice = IO.ShowSelectionMenu(
                "Choose an option",
                [
                    "View Products",
                    "Search By Name",
                    "Search By Category",
                    "Search By Price Range",
                    "View Cart",
                    "Wishlist",
                    "Order History",
                    "Logout",
                    "Exit"
                ]);

            switch (choice)
            {
                case "View Products":
                    new ProductController(
                        user,
                        productService,
                        cartService,
                        wishlistService)
                        .ShowAllProducts();
                    break;

                case "Search By Name":
                    new ProductController(
                        user,
                        productService,
                        cartService,
                        wishlistService)
                        .SearchByName();
                    break;

                case "Search By Category":
                    new ProductController(
                        user,
                        productService,
                        cartService,
                        wishlistService)
                        .SearchByCategory();
                    break;

                case "Search By Price Range":
                    new ProductController(
                        user,
                        productService,
                        cartService,
                        wishlistService)
                        .SearchByPriceRange();
                    break;

                case "View Cart":
                    new CartController(
                        cartService,
                        orderService,
                        invoiceService)
                        .Start(user);
                    break;

                case "Wishlist":
                    new WishlistController(
                        wishlistService)
                        .Start(user);
                    break;

                case "Order History":
                    new OrderController(orderService)
                        .Start(user);
                    break;

                case "Logout":
                    return;

                case "Exit":
                    IO.ExitApplication();
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
