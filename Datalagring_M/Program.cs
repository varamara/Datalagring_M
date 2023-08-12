using Datalagring_M.Contexts;
using Datalagring_M.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddTransient<CustomerService>()
    .AddTransient<MenuService>()
    .AddDbContext<DataContext>(options => options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mikah\\OneDrive\\Documents\\database_local.mdf;Integrated Security=True;Connect Timeout=30"))
    .BuildServiceProvider();

var menu = serviceProvider.GetRequiredService<MenuService>();

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Skapa en ny kund");
    Console.WriteLine("2. lägg till ärende");
    Console.WriteLine("3. Visa alla ärenden");
    Console.WriteLine("4. Hämta ärende");
    Console.WriteLine("5. Uppdatera status på ärende");
    Console.WriteLine("6. Radera ");
    Console.Write("Välj ett av följande alternativ (1-4): ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await menu.CreateAsync();
            break;

        case "2":
            Console.Clear();
            await menu.AddIncidentToCustomerAsync();
            break;

        case "3":
            Console.Clear();
            await menu.GetAllIncidentsAsync();
            break;

        case "4":
            Console.Clear();
            await menu.GetIncidentByEmailAsync();
            break;

        case "5":
            Console.Clear();
            await menu.UpdateIncidentStatusAsync();
            break;

        case "6":
            Console.Clear();
            await menu.DeleteAsync();
            break;
    }

    Console.WriteLine("\nTryck på valfri knapp för att fortsätta...");
    Console.ReadKey();
}
