using ShoppingApp.Constants;
using Spectre.Console;

namespace ShoppingApp.View;

public static class IO
{
    /// <summary>
 /// Writes a formatted Spectre.Console markup line to the console.
 /// </summary>
 /// <param name="content">The Spectre markup text to print.</param>
    public static void PrintInConsole(string content)
    {
        AnsiConsole.MarkupLine(content);
    }

    /// <summary>
    /// Prompts the user for a non-empty text input.
    /// </summary>
    /// <param name="prompt">The prompt label displayed to the user.</param>
    /// <returns>The trimmed string entered by the user.</returns>
    public static string GetStringInput(string prompt)
    {
        PrintEmptyLine();
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Input cannot be empty.[/]")
                .Validate(input => !string.IsNullOrWhiteSpace(input)))
            .Trim();
    }

    /// <summary>
    /// Prompts the user for a valid positive integer.
    /// </summary>
    /// <param name="prompt">The prompt label displayed to the user.</param>
    /// <returns>A validated integer value.</returns>
    public static int GetIntInput(string prompt)
    {
        PrintEmptyLine();

        return AnsiConsole.Prompt(
            new TextPrompt<int>(prompt)
                .PromptStyle("green")
                .ValidationErrorMessage(
                    "[red]Please enter a valid whole number.[/]")
                .Validate(value =>
                {
                    if (value <= 0)
                    {
                        return ValidationResult.Error(
                            "[red]Value must be greater than 0.[/]");
                    }

                    return ValidationResult.Success();
                }));
    }

    /// <summary>
    /// Prompts the user for a valid positive decimal value.
    /// </summary>
    /// <param name="prompt">The prompt label displayed to the user.</param>
    /// <returns>A validated decimal value.</returns>
    public static decimal GetDecimalInput(string prompt)
    {
        PrintEmptyLine();

        return AnsiConsole.Prompt(
            new TextPrompt<decimal>(prompt)
                .PromptStyle("green")
                .ValidationErrorMessage(
                    "[red]Please enter a valid amount.[/]")
                .Validate(value =>
                {
                    if (value <= 0)
                    {
                        return ValidationResult.Error(
                            "[red]Value must be greater than 0.[/]");
                    }

                    return ValidationResult.Success();
                }));
    }

    /// <summary>
    /// Prompts the user for a valid date in <c>yyyy-MM-dd</c> format.
    /// </summary>
    /// <param name="prompt">The prompt label displayed to the user.</param>
    /// <returns>A parsed <see cref="DateOnly"/> value.</returns>
    //public static DateOnly GetDateOnlyInput(string prompt)
    //{
    //    PrintEmptyLine();
    //    string rawDate = AnsiConsole.Prompt(
    //        new TextPrompt<string>(prompt)
    //            .PromptStyle("green")
    //            .ValidationErrorMessage("[red]Invalid date format. Please use yyyy-MM-dd (e.g., 2002-08-15).[/]")
    //            .Validate(input =>
    //                DateOnly.TryParseExact(
    //                    input.Trim(),
    //                    "yyyy-MM-dd",
    //                    CultureInfo.InvariantCulture,
    //                    DateTimeStyles.None,
    //                    out _)));

    //    return DateOnly.ParseExact(rawDate.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
    //}

    /// <summary>
    /// Displays an interactive arrow-key selection menu and returns the chosen option.
    /// </summary>
    /// <param name="title">The title shown above the menu choices.</param>
    /// <param name="options">The list of selectable menu options.</param>
    /// <returns>The selected option string.</returns>
    public static string ShowSelectionMenu(string title, string[] options, int size = 3)
    {
        PrintEmptyLine();
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(size)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .HighlightStyle(new Style(Color.Green, decoration: Decoration.Bold))
                .AddChoices(options));
    }

    /// <summary>
    /// Displays a Yes/No confirmation prompt using arrow keys.
    /// </summary>
    /// <param name="prompt">The confirmation question.</param>
    /// <returns><c>true</c> if the user selects Yes; otherwise, <c>false</c>.</returns>
    public static bool ConfirmAction(string prompt)
    {
        PrintEmptyLine();
        return AnsiConsole.Confirm(prompt);
    }

    /// <summary>
    /// Pauses execution until the user presses any key.
    /// </summary>
    public static void WaitInConsole()
    {
        PrintEmptyLine();
        AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    /// <summary>
    /// Displays a red error panel and waits for user acknowledgment.
    /// </summary>
    /// <param name="content">The error message to display.</param>
    public static void PrintInvalidMessage(string content)
    {
        PrintEmptyLine();
        var panel = new Panel($"[bold red] {Markup.Escape(content)}[/]")
        {
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Red),
        };
        AnsiConsole.Write(panel);
        WaitInConsole();
    }

    /// <summary>
    /// Displays a green success panel and waits for user acknowledgment.
    /// </summary>
    /// <param name="content">The success message to display.</param>
    public static void PrintSuccessMessage(string content)
    {
        PrintEmptyLine();
        var panel = new Panel($"[bold green] {Markup.Escape(content)}[/]")
        {
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Green),
        };
        AnsiConsole.Write(panel);
        WaitInConsole();
    }

    /// <summary>
    /// Writes a blank line to the console.
    /// </summary>
    public static void PrintEmptyLine()
    {
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Clears the console and renders an optional ASCII Fillet banner along with a styled rule header.
    /// </summary>
    /// <param name="header">The section header text.</param>
    /// <param name="bannerTitle">Optional large banner text rendered above the header.</param>
    public static void ShowHeader(string header, string bannerTitle = "MICROMAX")
    {
        ClearConsole();

        if (!string.IsNullOrWhiteSpace(bannerTitle))
        {
            AnsiConsole.Write(
                new FigletText(bannerTitle)
                    .Centered()
                    .Color(Color.Green1));
        }

        var rule = new Rule($"[bold yellow]{Markup.Escape(header)}[/]")
        {
            Justification = Justify.Center,
            Style = Style.Parse(Site.color),
        };
        AnsiConsole.Write(rule);
        PrintEmptyLine();
    }

    /// <summary>
    /// Clears all content from the console screen.
    /// </summary>
    public static void ClearConsole()
    {
        AnsiConsole.Clear();
    }

    /// <summary>
    /// Displays a goodbye panel before exiting the application.
    /// </summary>
    public static void ExitApplication()
    {
        ClearConsole();
        var panel = new Panel($"[{Site.color}]Thank you for choosing our shop! Come again.[/]")
        {
            Border = BoxBorder.Double,
            Padding = new Padding(2, 1),
            BorderStyle = new Style(Color.Green1),
        };
        AnsiConsole.Write(Align.Center(panel));
        WaitInConsole();
    }
}