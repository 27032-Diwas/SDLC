using ShoppingApp.Constants;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace ShoppingApp.View;

public class UserView
{
    /// <summary>
    /// Prompts for and validates a email (xyz@abc.com).
    /// </summary>
    /// <param name="prompt">The prompt message displayed to the user.</param>
    /// <returns>A validated, trimmed email string.</returns>
    public static string GetValidatedEmail(string prompt = $"[cyan]{UserPrompts.GetEmail}[/]")
    {
        IO.PrintEmptyLine();
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Error("[red]Email cannot be empty.[/]");
                    }

                    string trimmed = input.Trim();
                    if (!Regex.IsMatch(trimmed, RegexPatterns.EmailRegex))
                    {
                        return ValidationResult.Error($"[red]{ErrorMessages.InvalidEmail}.[/]");
                    }

                    return ValidationResult.Success();
                }))
            .Trim();
    }

    /// <summary>
    /// Prompts for and validates a phone number.
    /// </summary>
    /// <param name="prompt">The prompt message displayed to the user.</param>
    /// <returns>A validated, trimmed phone number string.</returns>
    public static string GetValidatedPhoneNumber(string prompt = $"[cyan]{UserPrompts.GetPhoneNumber}[/]")
    {
        IO.PrintEmptyLine();
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Error("[red]Phone number cannot be empty.[/]");
                    }

                    string trimmed = input.Trim();
                    if (!Regex.IsMatch(trimmed, RegexPatterns.PhoneNumberRegex))
                    {
                        return ValidationResult.Error($"[red]{ErrorMessages.InvalidPhoneNumber}[/]");
                    }

                    return ValidationResult.Success();
                }))
            .Trim();
    }

    /// <summary>
    /// Prompts for and validates a password (minimum 4 characters, no spaces).
    /// </summary>
    /// <param name="prompt">The prompt message displayed to the user.</param>
    /// <returns>A validated password string.</returns>
    public static string GetValidatedPassword(string prompt = "[cyan]Enter password:[/]")
    {
        IO.PrintEmptyLine();
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .Secret('*')
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Error("[red]Password cannot be empty.[/]");
                    }

                    if (input.Length < 4)
                    {
                        return ValidationResult.Error("[red]Password must be at least 4 characters long.[/]");
                    }

                    if (input.Contains(' '))
                    {
                        return ValidationResult.Error("[red]Password cannot contain spaces.[/]");
                    }

                    return ValidationResult.Success();
                }));
    }

    /// <summary>
    /// Prompts the user to confirm their password and validates immediately that it matches <paramref name="expectedPassword"/>.
    /// </summary>
    /// <param name="expectedPassword">The original password to match against.</param>
    /// <param name="prompt">The prompt message displayed to the user.</param>
    /// <returns>The confirmed password string.</returns>
    public static string GetConfirmedPassword(string expectedPassword, string prompt = "[cyan]Confirm password:[/]")
    {
        IO.PrintEmptyLine();
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .Secret('*')
                .Validate(input =>
                {
                    if (input != expectedPassword)
                    {
                        return ValidationResult.Error("[red]Passwords do not match. Please try again.[/]");
                    }

                    return ValidationResult.Success();
                }));
    }

    /// <summary>
    /// Prompts the user for a masked secret input (such as a password).
    /// </summary>
    /// <param name="prompt">The prompt label displayed to the user.</param>
    /// <returns>The secret string entered by the user.</returns>
    public static string GetSecretInput(string prompt = $"[cyan]{UserPrompts.GetPassword}[/]")
    {
        IO.PrintEmptyLine();
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .Secret('*')
                .ValidationErrorMessage("[red]Password cannot be empty.[/]")
                .Validate(input => !string.IsNullOrWhiteSpace(input)));
    }

    /// <summary>
    /// Prompts for and validates a PIN code.
    /// </summary>
    /// <param name="prompt">The prompt message displayed to the user.</param>
    /// <returns>A validated, trimmed PIN code string.</returns>
    public static string GetValidatedPincode(string prompt = $"[cyan]{UserPrompts.GetPincode}[/]")
    {
        IO.PrintEmptyLine();

        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Error("[red]PIN code cannot be empty.[/]");
                    }

                    string trimmed = input.Trim();

                    if (!Regex.IsMatch(trimmed, RegexPatterns.PincodeRegex))
                    {
                        return ValidationResult.Error($"[red]{ErrorMessages.InvalidPincode}[/]");
                    }

                    return ValidationResult.Success();
                }));
    }

    /// <summary>
    /// Prompts for and validates a name.
    /// </summary>
    /// <param name="prompt">The prompt message displayed to the user.</param>
    /// <returns>A validated, trimmed name string.</returns>
    public static string GetValidName(string prompt = $"[cyan]{UserPrompts.GetName}[/]")
    {
        IO.PrintEmptyLine();

        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Error("[red]Name cannot be empty.[/]");
                    }

                    string trimmed = input.Trim();

                    if (!Regex.IsMatch(trimmed, RegexPatterns.NameRegex))
                    {
                        return ValidationResult.Error($"[red]{ErrorMessages.InvalidName}[/]");
                    }

                    return ValidationResult.Success();
                }));
    }

    /// <summary>
    /// Prompts for and validates Address Line 1.
    /// </summary>
    /// <returns>A validated address string.</returns>
    public static string GetValidAddress(string prompt = $"[cyan]{UserPrompts.GetAddress}[/]")
    {
        IO.PrintEmptyLine();

        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Error("[red]Address cannot be empty.[/]");
                    }

                    if (input.Trim().Length < 5)
                    {
                        return ValidationResult.Error("[red]Address is too short.[/]");
                    }

                    return ValidationResult.Success();
                }));
    }
}
