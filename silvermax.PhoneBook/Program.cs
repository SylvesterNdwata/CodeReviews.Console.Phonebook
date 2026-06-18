using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using silvermax.PhoneBook;
using silvermax.PhoneBook.DbAcess;
using System;

public static class Program
{
    private static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<ContactDbContext>(opts =>
                    opts.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                services.AddTransient<UserInput>();
            })
            .Build();

        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContactDbContext>();

        await DataSeeder.SeedAsync(db);
        Console.Clear();

        var input = scope.ServiceProvider.GetRequiredService<UserInput>();
        var contactService = new ContactService(db, input, CancellationToken.None);

        UserInterface ui = new(contactService);
        await ui.Start();
    }
}
