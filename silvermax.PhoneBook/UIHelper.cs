using Spectre.Console;

namespace silvermax.PhoneBook;

internal static class UIHelper
{
    internal static void ContinueMessage()
    {
        AnsiConsole.MarkupLine("Press Any Key to continue...");
        Console.ReadKey();
    }
}
