using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface ICartService
{
    /// <summary>
    /// Retrieves the active shopping cart for the specified user.
    /// </summary>
    /// <param name="userId">Unique identifier of the user whose cart is requested.</param>
    /// <returns>The user's current cart, created if none exists.</returns>
    Cart GetCart(Guid userId);

    /// <summary>
    /// Adds the specified product and quantity to the user's active cart.
    /// </summary>
    /// <param name="userId">Identifier of the user performing the cart update.</param>
    /// <param name="productId">Identifier of the product being added.</param>
    /// <param name="quantity">Number of units to add to the cart.</param>
    void AddToCart(Guid userId, Guid productId, int quantity);

    /// <summary>
    /// Updates the quantity of an existing product in the user's cart.
    /// </summary>
    /// <param name="userId">Identifier of the user whose cart is being modified.</param>
    /// <param name="productId">Identifier of the product whose quantity is updated.</param>
    /// <param name="quantity">The new quantity to set for the product.</param>
    void UpdateQuantity(Guid userId, Guid productId, int quantity);

    /// <summary>
    /// Removes a product from the user's cart.
    /// </summary>
    /// <param name="userId">Identifier of the user whose cart is being modified.</param>
    /// <param name="productId">Identifier of the product to remove from the cart.</param>
    void RemoveProduct(Guid userId, Guid productId);

    /// <summary>
    /// Calculates the total cost of all products in the user's cart.
    /// </summary>
    /// <param name="userId">Identifier of the user whose cart total is being calculated.</param>
    /// <returns>The total amount for all items in the cart.</returns>
    decimal GetTotal(Guid userId);
}