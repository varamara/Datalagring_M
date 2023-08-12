using System;
using Datalagring_M.Services;

var menu = new MenuService();

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Skapa en ny kund");
    Console.WriteLine("2. Visa alla ärenden");
    Console.WriteLine("3. Hämta ärende");
    Console.WriteLine("4. Uppdatera status på ärende");
    Console.WriteLine("5. Radera ");
    Console.Write("Välj ett av följande alternativ (1-4): ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await menu.CreateAsync();
            break;

        case "2":
            Console.Clear();
            await menu.GetAllIncidentsAsync();
            break;

        case "3":
            Console.Clear();
            await menu.GetIncidentByEmailAsync();
            break;

        case "4":
            Console.Clear();
            await menu.UpdateIncidentStatusAsync();
            break;

        case "5":
            Console.Clear();
            await menu.DeleteAsync();
            break;
    }

    Console.WriteLine("\nTryck på valfri knapp för att fortsätta...");
    Console.ReadKey();
}
