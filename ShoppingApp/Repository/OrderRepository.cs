using System.Text.Json;
using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public class OrderRepository : IOrderRepository
{
    private const string FilePath = "Data/Orders.json";

    public void AddOrder(Order order)
    {
        List<Order> orders = this.GetAllOrders();

        orders.Add(order);

        this.SaveAllOrders(orders);
    }

    public List<Order> GetOrdersByUserId(Guid userId)
    {
        return this.GetAllOrders()
            .Where(order => order.UserId == userId)
            .ToList();
    }

    public Order? GetOrderById(Guid orderId)
    {
        return this.GetAllOrders()
            .FirstOrDefault(order =>
                order.OrderId == orderId);
    }

    public void UpdateOrder(Order updatedOrder)
    {
        List<Order> orders = this.GetAllOrders();

        int index = orders.FindIndex(
            order => order.OrderId == updatedOrder.OrderId);

        if (index >= 0)
        {
            orders[index] = updatedOrder;
        }

        this.SaveAllOrders(orders);
    }

    private List<Order> GetAllOrders()
    {
        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "[]");
        }

        string json = File.ReadAllText(FilePath);

        return JsonSerializer.Deserialize<List<Order>>(json)
               ?? [];
    }

    private void SaveAllOrders(List<Order> orders)
    {
        string json =
            JsonSerializer.Serialize(
                orders,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

        File.WriteAllText(FilePath, json);
    }
}