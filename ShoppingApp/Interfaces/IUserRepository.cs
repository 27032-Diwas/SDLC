using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    /// <param name="user">The user to be added.</param>
    /// <returns>True if the user was added successfully; otherwise, false.</returns>
    bool AddUser(User user);

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>The matching user if found; otherwise, null.</returns>
    User? GetUserByEmail(string email);

    void UpdateUser(User user);
}
