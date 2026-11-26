namespace ShoppingApp.Interfaces;

public interface IAdminService
{
    /// <summary>
    /// Performs a login check for the specified user credentials.
    /// </summary>
    /// <param name="email">The identifier used to locate the user account.</param>
    /// <param name="password">The secret key used to authenticate the user.</param>
    /// <returns>
    /// True if the login process completes successfully; otherwise, false.
    /// </returns>
    bool Login(string email, string password);
}