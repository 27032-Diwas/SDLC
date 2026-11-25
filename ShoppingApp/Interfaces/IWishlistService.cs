using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface IWishlistService
{
    void AddToWishlist(
        Guid userId,
        Guid productId);

    void RemoveFromWishlist(
        Guid userId,
        Guid productId);

    List<Product> GetWishlist(
        Guid userId);
}