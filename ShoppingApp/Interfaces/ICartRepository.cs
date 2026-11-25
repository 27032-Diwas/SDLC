using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface ICartRepository
{
    Cart GetByUserId(Guid userId);

    void Save(Cart cart);

    void Clear(Guid userId);
}
