using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public interface IOrderRepository
{
    void AddOrder(Order order);

    List<Order> GetOrdersByUserId(Guid userId);

    Order? GetOrderById(Guid orderId);

    void UpdateOrder(Order order);
}