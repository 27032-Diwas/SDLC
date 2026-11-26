using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;
using Spectre.Console;

namespace ShoppingApp.Controllers;

public class OrderController(
    IOrderService orderService)
{
    public void Start(User user)
    {
        while (true)
        {
            IO.ShowHeader("ORDER HISTORY");

            List<Order> orders =
                orderService.GetOrderHistory(
                    user.UserId);

            if (!orders.Any())
            {
                IO.PrintInvalidMessage("No orders found.");

                return;
            }

            this.DisplayOrders(orders);

            string[] options =
                orders.Select(
                    (order, index) =>
                    {
                        string firstProduct =
                            order.Items.Any()
                                ? order.Items.First().ProductName
                                : "Unknown Product";

                        int remainingProducts =
                            order.Items.Count - 1;

                        return remainingProducts > 0
                            ? $"{index + 1}. {firstProduct} (+{remainingProducts} more)"
                            : $"{index + 1}. {firstProduct}";
                    })
                .Append("Back")
                .ToArray();

            string selectedOption =
                IO.ShowSelectionMenu(
                    "Select Order",
                    options,
                    10);

            if (selectedOption == "Back")
            {
                return;
            }

            int selectedIndex =
                int.Parse(
                    selectedOption
                        .Split('.')[0])
                - 1;

            Order selectedOrder =
                orders[selectedIndex];

            this.ShowOrderDetails(
                selectedOrder);
        }
    }

    private void DisplayOrders(
        List<Order> orders)
    {
        Table table = new();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Products");
        table.AddColumn("Order Date");
        table.AddColumn("Amount");
        table.AddColumn("Status");

        foreach (Order order in orders)
        {
            string products =
                string.Join(
                    ", ",
                    order.Items.Select(
                        item => item.ProductName));

            table.AddRow(
                products,
                order.OrderDate.ToString("dd-MM-yyyy"),
                $"Rs. {order.TotalAmount}",
                order.Status);
        }

        AnsiConsole.Write(table);

        IO.PrintEmptyLine();
    }

    private void ShowOrderDetails(
        Order order)
    {
        IO.ShowHeader("ORDER DETAILS");

        IO.PrintInConsole(
            $"[cyan]Order Date:[/] [green]{order.OrderDate:dd-MM-yyyy HH:mm}[/]");

        IO.PrintInConsole(
            $"[cyan]Status:[/] [green]{order.Status}[/]");

        IO.PrintInConsole(
            $"[cyan]Total Amount:[/] [green]Rs. {order.TotalAmount}[/]");

        IO.PrintEmptyLine();

        Table table = new();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Product");
        table.AddColumn("Quantity");
        table.AddColumn("Unit Price");
        table.AddColumn("Total");

        foreach (CartItem item in order.Items)
        {
            table.AddRow(
                item.ProductName,
                item.Quantity.ToString(),
                $"Rs. {item.UnitPrice}",
                $"Rs. {item.TotalPrice}");
        }

        AnsiConsole.Write(table);

        IO.PrintEmptyLine();

        string choice =
            IO.ShowSelectionMenu(
                "Choose Action",
                [
                    "Return Order",
                    "Back"
                ]);

        switch (choice)
        {
            case "Return Order":

                if (order.Status == "Returned")
                {
                    IO.PrintInvalidMessage("Order already returned.");

                    return;
                }

                bool result =
                    orderService.ReturnOrder(
                        order.OrderId);

                if (result)
                {
                    IO.PrintSuccessMessage("Order returned successfully.");
                }
                else
                {
                    IO.PrintInvalidMessage("Unable to return order.");
                }

                break;

            case "Back":
                return;
        }
    }
}