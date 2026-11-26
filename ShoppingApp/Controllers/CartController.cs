using ShoppingApp.Constants;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

namespace ShoppingApp.Controllers;

public class CartController(
    IProductService productService,
    ICartService cartService,
    IOrderService orderService,
    IInvoiceService invoiceService,
    IInvoicePdfService invoicePdfService,
    IUserService userService)
{
    public void Start(User user)
    {
        while (true)
        {
            Cart cart =
                cartService.GetCart(user.UserId);

            IO.ShowHeader("MY CART");


            if (!cart.Items.Any())
            {
                IO.PrintInvalidMessage("Cart is empty");
                return;
            }

            CartView.DisplayCart(cart);

            string choice =
                IO.ShowSelectionMenu(
                    "Choose an option",
                    [
                        "Update Quantity",
                        "Remove Product",
                        "Checkout",
                        "Back"
                    ],4);

            switch (choice)
            {
                case "Update Quantity":
                    this.UpdateQuantity(user, cart);
                    break;

                case "Remove Product":
                    this.RemoveProduct(user, cart);
                    break;

                case "Checkout":
                    this.Checkout(user);
                    break;

                case "Back":
                    return;
            }
        }
    }

    private void UpdateQuantity(
    User user,
    Cart cart)
    {
        try
        {
            string selectedProduct =
                IO.ShowSelectionMenu(
                    "Select Product",
                    cart.Items
                        .Select(item => item.ProductName)
                        .ToArray());

            CartItem item =
                cart.Items.First(
                    item => item.ProductName == selectedProduct);

            Product? product =
                productService.GetProductById(
                    item.ProductId);

            if (product == null)
            {
                IO.PrintInvalidMessage(
                    "Product not found.");

                return;
            }

            int quantity =
                IO.GetIntInput(
                    $"Enter new quantity (Available Stock: {product.Stock})");

            cartService.UpdateQuantity(
                user.UserId,
                item.ProductId,
                quantity);

            IO.PrintSuccessMessage(
                "Quantity updated successfully.");
        }
        catch (InvalidOperationException ex)
        {
            IO.PrintInvalidMessage(
                ex.Message);
        }
    }


    private void RemoveProduct(
        User user,
        Cart cart)
    {
        string selectedProduct =
            IO.ShowSelectionMenu(
                "Select Product",
                cart.Items
                    .Select(item => item.ProductName)
                    .ToArray());

        CartItem item =
            cart.Items.First(
                item => item.ProductName == selectedProduct);

        cartService.RemoveProduct(
            user.UserId,
            item.ProductId);

        IO.PrintSuccessMessage(
            "Product removed successfully.");
    }

    private void Checkout(User user)
    {
        bool confirm =
            IO.ConfirmAction(
                "Proceed to checkout?");

        if (!confirm)
        {
            return;
        }

        if (user.Address == null)
        {
            IO.PrintInConsole(
                "[yellow]Delivery address required.[/]");

            Address address =
                this.GetAddress();

            user.Address = address;

            userService.UpdateUser(user);
        }

        Order order =
            orderService.Checkout(
                user.UserId);

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
            $"Invoice PDF generated successfully.\nLocation: {pdfPath}");
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