using ShoppingApp.Models;
using ShoppingApp.Services;
using ShoppingApp.View;

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
                IO.PrintInvalidMessage(
                    "No orders found.");

                return;
            }

            string selectedOrder =
                IO.ShowSelectionMenu(
                    "Select Order",
                    orders
                        .Select(order =>
                            $"{order.OrderId} | ₹{order.TotalAmount}")
                        .Append("Back")
                        .ToArray());

            if (selectedOrder == "Back")
            {
                return;
            }

            Order order =
                orders.First(
                    x => selectedOrder.Contains(
                        x.OrderId.ToString()));

            this.ShowOrderDetails(order);
        }
    }

    private void ShowOrderDetails(
        Order order)
    {
        IO.ShowHeader("ORDER DETAILS");

        IO.PrintInConsole(
            $"[cyan]Order Id:[/] {order.OrderId}");

        IO.PrintInConsole(
            $"[cyan]Order Date:[/] {order.OrderDate}");

        IO.PrintInConsole(
            $"[cyan]Status:[/] [green]{order.Status}[/]");

        IO.PrintInConsole(
            $"[cyan]Total:[/] ₹{order.TotalAmount}");

        IO.PrintEmptyLine();

        foreach (CartItem item in order.Items)
        {
            IO.PrintInConsole(
                $"• {item.ProductName} | Qty : {item.Quantity} | ₹{item.TotalPrice}");
        }

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
                    IO.PrintInvalidMessage(
                        "Order already returned.");

                    return;
                }

                bool result =
                    orderService.ReturnOrder(
                        order.OrderId);

                if (result)
                {
                    IO.PrintSuccessMessage(
                        "Order returned successfully.");
                }
                else
                {
                    IO.PrintInvalidMessage(
                        "Unable to return order.");
                }

                break;

            case "Back":
                return;
        }
    }
}