using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Repository;

namespace ShoppingApp.Services;

public class WishlistService(
    IWishlistRepository wishlistRepository,
    IProductRepository productRepository)
    : IWishlistService
{
    public void AddToWishlist(
        Guid userId,
        Guid productId)
    {
        WishlistItem item = new()
        {
            UserId = userId,
            ProductId = productId,
            AddedOn = DateTime.Now
        };

        wishlistRepository.Add(item);
    }

    public void RemoveFromWishlist(
        Guid userId,
        Guid productId)
    {
        wishlistRepository.Remove(
            userId,
            productId);
    }

    public List<Product> GetWishlist(
        Guid userId)
    {
        List<WishlistItem> wishlistItems =
            wishlistRepository.GetByUserId(
                userId);

        List<Product> products = [];

        foreach (WishlistItem item in wishlistItems)
        {
            Product? product =
                productRepository.GetProductById(
                    item.ProductId);

            if (product != null)
            {
                products.Add(product);
            }
        }

        return products;
    }
}