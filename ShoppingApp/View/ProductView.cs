using ShoppingApp.Models;
using Spectre.Console;

namespace ShoppingApp.View;
public static class ProductView
{
    public static void DisplayProducts(
        List<Product> products)
    {
        Table table = new();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Name");
        table.AddColumn("Category");
        table.AddColumn("Price");
        table.AddColumn("Stock");

        foreach (Product product in products)
        {
            table.AddRow(
                product.Name,
                product.Category,
                $"Rs. {product.Price}",
                product.Stock.ToString());
        }

        AnsiConsole.Write(table);
    }
}
