using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public interface IWishlistRepository
{
    List<WishlistItem> GetByUserId(Guid userId);

    void Add(WishlistItem item);

    void Remove(Guid userId, Guid productId);
}