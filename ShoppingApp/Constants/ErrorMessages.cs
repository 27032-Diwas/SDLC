namespace ShoppingApp.Constants;

public static class ErrorMessages
{
    /// <summary>
    /// Represents the message displayed when user enters an invalid option.
    /// </summary>
    public const string InvalidOption = "ENTER A VALID OPTION";

    /// <summary>
    /// Represents the message displayed when user enters an invalid string.
    /// </summary>
    public const string InvalidString = "ENTER A VALUE";

    /// <summary>
    /// Represents the message displayed when the user enters an invalid name.
    /// </summary>
    public const string InvalidName = "Please enter a valid name (2-20 characters). Only letters, numbers, spaces, apostrophes, and hyphens are allowed.";

    /// <summary>
    /// Represents the message displayed when the user enters an invalid email.
    /// </summary>
    public const string InvalidEmail = "Please enter a valid email address (xyz@abc.com)";
    /// <summary>
    /// Represents the message displayed when the user enter an invalid password.
    /// </summary>
    public const string InvalidPassword = "Password must be 8 to 20 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";

    /// <summary>
    /// Represents the message displayed when the user enter an invalid email or password.
    /// </summary>
    public const string InvalidCredentials = "Invalid email or password";

    /// <summary>
    /// Represents the message displayed when the user enter an invalid confirm password.
    /// </summary>
    public const string InvalidConfirmPassword = "Password does't match";

    /// <summary>
    /// Represents the message displayed when user enter already existing id.
    /// </summary>
    public const string AccountAlreadyExist = "Email is already registered.";

    /// <summary>
    /// Represents the message displayed when user enters an invalid number.
    /// </summary>
    public const string InvalidNumber = "Enter a valid number";

    public const string InvalidPhoneNumber = "The phone number must contain exactly 10 digits and start with a digit from 6 to 9";

    public const string InvalidPincode = "Please enter a valid 6-digit PIN code.";
}
