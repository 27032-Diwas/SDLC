using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface IOrderService
{
    /// <summary>
    /// Creates an order from all items currently in the user's cart.
    /// </summary>
    /// <param name="userId">Identifier of the user placing the order.</param>
    /// <returns>The newly created order.</returns>
    Order Checkout(Guid userId);

    /// <summary>
    /// Retrieves the order history for a specific user.
    /// </summary>
    /// <param name="userId">Identifier of the user whose order history is requested.</param>
    /// <returns>A list of orders placed by the user.</returns>
    List<Order> GetOrderHistory(Guid userId);

    /// <summary>
    /// Processes a return request for the specified order.
    /// </summary>
    /// <param name="orderId">Identifier of the order to be returned.</param>
    /// <returns>True if the return was successful; otherwise, false.</returns>
    bool ReturnOrder(Guid orderId);

    /// <summary>
    /// Retrieves all orders from the system.
    /// </summary>
    /// <returns>A list of all orders.</returns>
    List<Order> GetAllOrders();

    /// <summary>
    /// Places an order directly for a specific product without using the cart.
    /// </summary>
    /// <param name="userId">Identifier of the user placing the order.</param>
    /// <param name="productId">Identifier of the product being ordered.</param>
    /// <param name="quantity">Quantity of the product to order.</param>
    /// <returns>The newly created order.</returns>
    Order PlaceDirectOrder(Guid userId, Guid productId, int quantity);
}
