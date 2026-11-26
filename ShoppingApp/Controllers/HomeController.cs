using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

namespace ShoppingApp.Controllers;

public class HomeController(
    User user,
    IUserService userService,
    IProductService productService,
    ICartService cartService,
    IOrderService orderService,
    IWishlistService wishlistService,
    IInvoiceService invoiceService,
    IInvoicePdfService invoicePdfService)
{
    private readonly ProductController productController = new ProductController(
                        user,
                        userService,
                        productService,
                        cartService,
                        wishlistService,
                        orderService,
                        invoiceService,
                        invoicePdfService);

    private readonly WishlistController wishlistController = new WishlistController(wishlistService, cartService, orderService, invoiceService, invoicePdfService, userService);
    private readonly CartController cartController = new CartController(
                        productService,
                        cartService,
                        orderService,
                        invoiceService,
                        invoicePdfService,
                        userService);

    private readonly OrderController orderController = new OrderController(orderService);

    public void Start()
    {
        while (true)
        {
            IO.ShowHeader($"WELCOME {user.Name.ToUpper()}");

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
                ], 9);

            switch (choice)
            {
                case "View Products":
                    productController.ShowAllProducts();
                    break;

                case "Search By Name":
                    productController.SearchByName();
                    break;

                case "Search By Category":
                    productController.SearchByCategory();
                    break;

                case "Search By Price Range":
                    productController.SearchByPriceRange();
                    break;

                case "View Cart":
                    cartController.Start(user);
                    break;

                case "Wishlist":
                    wishlistController.Start(user);
                    break;

                case "Order History":
                    orderController.Start(user);
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
