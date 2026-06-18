using System.ComponentModel.DataAnnotations;
using Spectre.Console;
using PhoneNumbers;
using silvermax.PhoneBook.Dtos;

namespace silvermax.PhoneBook;

public class UserInput
{
    public UserInputDto GetInfo()
    {
        var email = new EmailAddressAttribute();
        string name = AnsiConsole.Ask<string>("Please input the name:");

        string emailInput = AnsiConsole.Prompt(
            new TextPrompt<string>("Please enter the email")
            .Validate(input =>
            {
                if (!email.IsValid(input) | string.IsNullOrWhiteSpace(input))
                    return Spectre.Console.ValidationResult.Error("Please enter a valid email address, i.e example@domain.com");

                return Spectre.Console.ValidationResult.Success();
            }));

        var phoneNumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Please enter the phone number(i.e. +49..., starting with country + and country code):")
            .Validate(input =>
            {
                if (!IsValidPhoneNr(input))
                    return Spectre.Console.ValidationResult.Error("Please enter a valid phone number.(i.e. +49..., starting with country + and country code)");
                return Spectre.Console.ValidationResult.Success();
            }));

        return new UserInputDto
        {
            Name = name,
            Email = emailInput,
            PhoneNumber = phoneNumber
        };
    }

    private bool IsValidPhoneNr(string number)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
        var phoneNumber = phoneUtil.Parse(number, null);
        return phoneUtil.IsValidNumber(phoneNumber);
    }
}
