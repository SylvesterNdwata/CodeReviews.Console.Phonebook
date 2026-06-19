using silvermax.PhoneBook.DbAcess;
using Spectre.Console;
using static silvermax.PhoneBook.Enums;

namespace silvermax.PhoneBook;

public class UserInterface(ContactService service, MailService mailService)
{
    public async Task Start()
    {
        bool openBook = true;
        while (openBook)
        {
            Console.Clear();
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<Menu>()
            .Title("Welcome to the PhoneBook.")
            .UseConverter(e => Enums.ToDisplayName(e))
            .AddChoices(Enum.GetValues<Menu>()));

            switch (choice)
            {
                case Menu.AddContact:
                    await service.AddContactToDb();
                    UIHelper.ContinueMessage();
                    break;

                case Menu.UpdateContact:
                    await service.UpdateContact();
                    break;

                case Menu.ListContacts:
                    await service.DisplayContacts();
                    UIHelper.ContinueMessage();
                    break;

                case Menu.DeleteContact:
                    await service.DeleteContact();
                    break;

                case Menu.SendMail:
                    await mailService.SendMail();
                    break;

                case Menu.Exit:
                    openBook = false;
                    break;
            }
        }
    }
}
