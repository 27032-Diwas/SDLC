using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface IWishlistService
{
    /// <summary>
    /// Adds a product to the user's wishlist.
    /// </summary>
    /// <param name="userId">Identifier of the user whose wishlist is being modified.</param>
    /// <param name="productId">Identifier of the product to add to the wishlist.</param>
    void AddToWishlist(Guid userId, Guid productId);

    /// <summary>
    /// Removes a product from the user's wishlist.
    /// </summary>
    /// <param name="userId">Identifier of the user whose wishlist is being modified.</param>
    /// <param name="productId">Identifier of the product to remove from the wishlist.</param>
    void RemoveFromWishlist(Guid userId, Guid productId);

    /// <summary>
    /// Retrieves all products in the user's wishlist.
    /// </summary>
    /// <param name="userId">Identifier of the user whose wishlist is requested.</param>
    /// <returns>A list of products currently in the user's wishlist.</returns>
    List<Product> GetWishlist(Guid userId);
}