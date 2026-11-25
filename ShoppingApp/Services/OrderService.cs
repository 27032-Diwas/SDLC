using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Repository;

namespace ShoppingApp.Services;

public class OrderService(
    IOrderRepository orderRepository,
    ICartRepository cartRepository,
    IProductRepository productRepository)
    : IOrderService
{
    public Order Checkout(Guid userId)
    {
        Cart cart =
            cartRepository.GetByUserId(userId);

        if (!cart.Items.Any())
        {
            throw new Exception(
                "Cart is empty.");
        }

        Order order = new()
        {
            OrderId = Guid.NewGuid(),
            UserId = userId,
            OrderDate = DateTime.Now,
            Items = cart.Items.ToList(),
            TotalAmount = cart.CartTotal,
            Status = "Placed"
        };

        foreach (CartItem item in cart.Items)
        {
            Product? product =
                productRepository.GetProductById(
                    item.ProductId);

            if (product == null)
            {
                continue;
            }

            product.Stock -= item.Quantity;

            productRepository.UpdateProduct(
                product);
        }

        orderRepository.AddOrder(order);

        cartRepository.Clear(userId);

        return order;
    }

    public List<Order> GetOrderHistory(Guid userId)
    {
        return orderRepository.GetOrdersByUserId(userId);
    }

    public bool ReturnOrder(Guid orderId)
    {
        Order? order =
            orderRepository.GetOrderById(orderId);

        if (order == null)
        {
            return false;
        }

        if (order.Status == "Returned")
        {
            return false;
        }

        order.Status = "Returned";

        orderRepository.UpdateOrder(order);

        return true;
    }
}