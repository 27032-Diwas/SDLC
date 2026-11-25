using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface ICartService
{
    Cart GetCart(Guid userId);

    void AddToCart(
        Guid userId,
        Guid productId,
        int quantity);

    void UpdateQuantity(
        Guid userId,
        Guid productId,
        int quantity);

    void RemoveProduct(
        Guid userId,
        Guid productId);

    decimal GetTotal(Guid userId);
}