using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public interface IWishlistRepository
{
    /// <summary>
    /// Retrieves all wishlist items associated with a specific user.
    /// </summary>
    /// <param name="userId">Identifier of the user whose wishlist items are requested.</param>
    /// <returns>A list of wishlist items belonging to the specified user.</returns>
    List<WishlistItem> GetByUserId(Guid userId);

    /// <summary>
    /// Adds a product to the user's wishlist.
    /// </summary>
    /// <param name="item">The wishlist item to be added.</param>
    void Add(WishlistItem item);

    /// <summary>
    /// Removes a product from the user's wishlist.
    /// </summary>
    /// <param name="userId">Identifier of the user whose wishlist is being modified.</param>
    /// <param name="productId">Identifier of the product to remove from the wishlist.</param>
    void Remove(Guid userId, Guid productId);
}