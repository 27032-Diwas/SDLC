using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface IOrderService
{
    Order Checkout(Guid userId);

    List<Order> GetOrderHistory(Guid userId);

    bool ReturnOrder(Guid orderId);
}
