using ShoppingApp.Constants;
using System.Text.RegularExpressions;

namespace ShoppingApp.Helpers;

public static class UserValidation
{
    /// <summary>
    /// Determines whether the specified user name is valid.
    /// </summary>
    /// <param name="name"> Name of user. </param>
    /// <returns> True if the name matches the required pattern; otherwise false. </returns>
    public static bool IsValidName(string name) => !string.IsNullOrEmpty(name)
                                                   && Regex.IsMatch(name, RegexPatterns.NameRegex);

    /// <summary>
    /// Determines whether the specified user email is valid.
    /// </summary>
    /// <param name="employeeID"> Email of user. </param>
    /// <returns> True if the email matches the required pattern; otherwise false. </returns>
    public static bool IsValidEmail(string email) => !string.IsNullOrEmpty(email)
                                                     && Regex.IsMatch(email, RegexPatterns.EmailRegex);

    /// <summary>
    /// Determines whether the specified user password is valid.
    /// </summary>
    /// <param name="password"> Email address of user. </param>
    /// <returns> True if the password matches the required pattern; otherwise false. </returns>
    public static bool IsValidPassword(string password) => !string.IsNullOrEmpty(password)
                                                           && Regex.IsMatch(password, RegexPatterns.PasswordRegex);
}
