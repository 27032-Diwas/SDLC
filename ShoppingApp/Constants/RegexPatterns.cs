namespace ShoppingApp.Constants;

/// <summary>
/// Contains all regex patterns.
/// </summary>
public static class RegexPatterns
{

    /// <summary>
    /// Regular expression pattern used to validate user email addresses.
    /// </summary>
    public const string EmailRegex = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

    /// <summary>
    /// Regular expression pattern used to validate a user phone number.
    /// The phone number must contain exactly 10 digits and start with a digit from 6 to 9.
    /// </summary>
    public const string PhoneNumberRegex = @"^[6-9]\d{9}$";

    /// <summary>
    /// Regular expression pattern used to validate user names.
    /// The name must:
    /// - Contain at least one alphabetic character.
    /// - Be between 2 and 20 characters long.
    /// - Allow letters, numbers, spaces, apostrophes, and hyphens.
    ///  </summary>
    public const string NameRegex = @"^(?=.*[A-Za-z])[A-Za-z0-9\s'-]{2,20}$";

    /// <summary>
    /// Regular expression pattern used to validate user password.
    /// The password must contain at least one uppercase letter, one lowercase letter,
    /// one numeric digit, one special character, and be between 8 and 20 characters long.
    /// </summary>
    public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

    public const string PincodeRegex = @"^[1-9][0-9]{5}$";
}
