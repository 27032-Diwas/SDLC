using ShoppingApp.Controllers;
using ShoppingApp.Interfaces;
using ShoppingApp.Repository;
using ShoppingApp.Services;

namespace ShoppingApp;

public class Program
{
    public static void Main()
    {
        IUserRepository userRepository =
    new UserRepository();

        IProductRepository productRepository =
            new ProductRepository();

        ICartRepository cartRepository =
            new CartRepository();

        IOrderRepository orderRepository =
            new OrderRepository();

        IWishlistRepository wishlistRepository =
            new WishlistRepository();

        IInvoiceRepository invoiceRepository = new InvoiceRepository();

        IUserService userService =
    new UserService(
        userRepository);

        IProductService productService =
            new ProductService(
                productRepository);

        ICartService cartService =
            new CartService(
                cartRepository,
                productRepository);

        IOrderService orderService =
            new OrderService(
                orderRepository,
                cartRepository,
                productRepository);

        IWishlistService wishlistService =
            new WishlistService(
                wishlistRepository,
                productRepository);

        IInvoiceService invoiceService = new InvoiceService(invoiceRepository);

        UserController userController =
    new UserController(
        userService,
        productService,
        cartService,
        orderService,
        wishlistService,
        invoiceService);

        userController.Start();
    }
}
