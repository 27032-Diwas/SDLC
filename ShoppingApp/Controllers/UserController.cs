using ShoppingApp.Constants;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;
using System.Runtime.CompilerServices;

namespace ShoppingApp.Controllers;

public class UserController
{
    private readonly IUserService userService;

    private readonly IProductService productService;

    private readonly ICartService cartService;

    private readonly IOrderService orderService;

    private readonly IWishlistService wishlistService;

    private readonly IInvoiceService invoiceService;

    private readonly IInvoicePdfService invoicePdfService;

    private readonly IAdminService adminService;
    public UserController(
    IUserService userService,
    IProductService productService,
    ICartService cartService,
    IOrderService orderService,
    IWishlistService wishlistService,
    IInvoiceService invoiceService,
    IInvoicePdfService invoicePdfService,
    IAdminService adminService)
    {
        this.userService = userService;
        this.productService = productService;
        this.cartService = cartService;
        this.orderService = orderService;
        this.wishlistService = wishlistService;
        this.invoiceService = invoiceService;
        this.invoicePdfService = invoicePdfService;
        this.adminService = adminService;
    }

    /// <summary>
    /// Starts the authentication menu..
    /// </summary>
    public void Start()
    {
        while (true)
        {
            IO.ShowHeader("AUTHENTICATION", "SHOPPING APP");
            string choice = IO.ShowSelectionMenu(
                "[bold cyan]Select an option using Up/Down arrow keys:[/]",
                new[] { Configurables.SignUp, Configurables.LogIn, Configurables.Exit });
            switch (choice)
            {
                case Configurables.SignUp:
                    this.HandleSignUp();
                    break;

                case Configurables.LogIn:
                    this.HandleLogin();
                    break;

                case Configurables.Exit:
                    IO.ExitApplication();
                    return;
            }
        }
    }

    public void HandleSignUp()
    {
        IO.ShowHeader("SIGN UP", "SHOPPING APP");
        string Name = UserView.GetValidName();
        string email = UserView.GetValidatedEmail();
        string phoneNumber = UserView.GetValidatedPhoneNumber();
        string password = UserView.GetValidatedPassword();
        string confirmPassword = UserView.GetConfirmedPassword(password);
        string houseNo = UserView.GetValidAddress();


        string selectedState = IO.ShowSelectionMenu(
            "Select State",
            StateDistrictData.StatesAndDistricts.Keys.ToArray());

        IO.PrintInConsole($"[cyan]State:[/] [green]{selectedState}[/]");
        string selectedDistrict = IO.ShowSelectionMenu(
            "Select District",
            StateDistrictData.StatesAndDistricts[selectedState].ToArray());
        IO.PrintInConsole($"[cyan]District:[/] [green]{selectedDistrict}[/]");
        string pinCode = UserView.GetValidatedPincode();

        Address address = new Address(houseNo, selectedDistrict, selectedState, pinCode);

        bool result = this.userService.RegisterUser(
            Name,
            email,
            phoneNumber,
            password,
            address);

        if (result)
        {
            IO.PrintSuccessMessage("Sign up successful! You can now log in.");
        }
    }

    public void HandleLogin()
    {
        IO.ShowHeader("LOGIN", "SHOPPING APP");

        string email = UserView.GetValidatedEmail();
        string password = UserView.GetSecretInput();

        if (this.adminService.Login(
        email,
        password))
        {
            IO.PrintSuccessMessage(
                "Welcome Admin.");

            AdminController adminController =
                new(
                    this.productService,
                    this.orderService);

            adminController.Start();

            return;
        }

        User? user = this.userService.Login(
            email,
            password);

        if (user == null)
        {
            IO.PrintInvalidMessage(ErrorMessages.InvalidCredentials);
            return;
        }

        //  IO.PrintSuccessMessage($"Welcome back, {user.Name}!");

        HomeController homeController = new HomeController(
        user,
        this.productService,
        this.cartService,
        this.orderService,
        this.wishlistService,
        this.invoiceService,
        this.invoicePdfService);

        homeController.Start();
    }
}
