using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface ICartRepository
{
    /// <summary>
    /// Retrieves the active cart for the specified user to continue the workflow.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose cart is being loaded.</param>
    /// <returns>The user's current cart, enabling subsequent workflow steps.</returns>
    Cart GetByUserId(Guid userId);

    /// <summary>
    /// Saves the current state of the cart to persist workflow progress.
    /// </summary>
    /// <param name="cart">The cart instance containing updated workflow data.</param>
    void Save(Cart cart);

    /// <summary>
    /// Clears the user's cart to reset workflow state for a new process.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose cart workflow is being cleared.</param>
    void Clear(Guid userId);
}
