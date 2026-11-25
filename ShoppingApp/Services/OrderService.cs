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

    public List<Order> GetAllOrders()
    {
        return orderRepository.GetAllOrders();
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

    public Order PlaceDirectOrder(
    Guid userId,
    Guid productId,
    int quantity)
    {
        Product? product =
            productRepository.GetProductById(
                productId);

        if (product == null)
        {
            throw new Exception(
                "Product not found.");
        }

        if (quantity <= 0)
        {
            throw new Exception(
                "Quantity must be greater than zero.");
        }

        if (quantity > product.Stock)
        {
            throw new Exception(
                "Insufficient stock.");
        }

        product.Stock -= quantity;

        productRepository.UpdateProduct(
            product);

        Cart cart =
            cartRepository.GetByUserId(
                userId);

        CartItem? cartItem =
            cart.Items.FirstOrDefault(
                item => item.ProductId == productId);

        if (cartItem != null)
        {
            if (cartItem.Quantity <= quantity)
            {
                cart.Items.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity -= quantity;
            }

            cartRepository.Save(cart);
        }

        CartItem orderItem = new()
        {
            ProductId = product.ProductId,
            ProductName = product.Name,
            UnitPrice = product.Price,
            Quantity = quantity
        };

        Order order = new()
        {
            OrderId = Guid.NewGuid(),
            UserId = userId,
            Items = [orderItem],
            TotalAmount = orderItem.TotalPrice,
            OrderDate = DateTime.Now,
            Status = "Placed"
        };

        orderRepository.AddOrder(order);

        return order;
    }

}