using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Registers a new user with the provided personal and address information.
    /// </summary>
    /// <param name="name">The full name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="phoneNumber">The phone number of the user.</param>
    /// <param name="password">The password for the user account.</param>
    /// <param name="address">The address information of the user.</param>
    /// <returns>True if the registration was successful; otherwise, false.</returns>
    bool RegisterUser(string name, string email, string phoneNumber, string password, Address address);

    /// <summary>
    /// Authenticates a user using their email address and password.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>The authenticated user if the credentials are valid; otherwise, null.</returns>
    User? Login(string email, string password);
}
