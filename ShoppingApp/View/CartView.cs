using ShoppingApp.Models;
using Spectre.Console;

namespace ShoppingApp.View;

public static class CartView
{
    public static void DisplayCart(
        Cart cart)
    {
        Table table = new();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Product");
        table.AddColumn("Price");
        table.AddColumn("Quantity");
        table.AddColumn("Total");

        foreach (CartItem item in cart.Items)
        {
            table.AddRow(
                item.ProductName,
                $"Rs. {item.UnitPrice}",
                item.Quantity.ToString(),
                $"Rs. {item.TotalPrice}");
        }

        AnsiConsole.Write(table);

        IO.PrintInConsole($"[bold green]Cart Total : Rs. {cart.CartTotal}[/]");
    }
}