using ShoppingApp.Models;
using Spectre.Console;

namespace ShoppingApp.View;

public static class InvoiceView
{
    public static void DisplayInvoice(
        Invoice invoice)
    {
        IO.ShowHeader("INVOICE");
        IO.PrintInConsole($"[cyan]Invoice Number:[/] {invoice.InvoiceId}");
        IO.PrintInConsole($"[cyan]Customer:[/] {invoice.CustomerName}");
        IO.PrintInConsole($"[cyan]Date:[/] {invoice.InvoiceDate}");
        IO.PrintEmptyLine();

        Table table = new();

        table.AddColumn("Product");
        table.AddColumn("Qty");
        table.AddColumn("Unit Price");
        table.AddColumn("Total");

        foreach (CartItem item in invoice.Items)
        {
            table.AddRow(
                item.ProductName,
                item.Quantity.ToString(),
                $"Rs. {item.UnitPrice}",
                $"Rs. {item.TotalPrice}");
        }

        AnsiConsole.Write(table);

        IO.PrintEmptyLine();
        IO.PrintInConsole($"[green]Products:[/] Rs. {invoice.ProductAmount}");
        IO.PrintInConsole($"[yellow]Delivery:[/] Rs. {invoice.DeliveryCharge}");
        IO.PrintInConsole($"[red]Discount:[/] Rs. {invoice.DiscountAmount}");
        IO.PrintInConsole($"[bold green]Final Amount:[/] Rs. {invoice.FinalAmount}");
    }
}