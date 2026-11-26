using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public interface IOrderRepository
{
    /// <summary>
    /// Adds a new order to the order repository.
    /// </summary>
    /// <param name="order">The order to be stored.</param>
    void AddOrder(Order order);

    /// <summary>
    /// Retrieves all orders associated with a specific user.
    /// </summary>
    /// <param name="userId">Identifier of the user whose orders are requested.</param>
    /// <returns>A list of orders belonging to the specified user.</returns>
    List<Order> GetOrdersByUserId(Guid userId);

    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="orderId">Identifier of the order to retrieve.</param>
    /// <returns>The matching order if found; otherwise, null.</returns>
    Order? GetOrderById(Guid orderId);

    /// <summary>
    /// Updates an existing order in the order repository.
    /// </summary>
    /// <param name="order">The order containing the updated information.</param>
    void UpdateOrder(Order order);

    /// <summary>
    /// Retrieves all orders from the order repository.
    /// </summary>
    /// <returns>A list of all orders.</returns>
    List<Order> GetAllOrders();
}