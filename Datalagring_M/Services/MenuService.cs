
using Datalagring_M.Contexts;
using Datalagring_M.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_M.Services;

internal class MenuService
{
    private readonly DataContext _context;
    private readonly CustomerService _customerService;

    public MenuService(CustomerService customerService, DataContext context)
    {
        _customerService = customerService;
        _context = context;
    }

    public async Task CreateAsync()
    {
        var customerEntity = new CustomerEntity();

        Console.Write("Förnamn: ");
        customerEntity.FirstName = Console.ReadLine() ?? "";

        Console.Write("Efternamn: ");
        customerEntity.LastName = Console.ReadLine() ?? "";

        Console.Write("E-postadress: ");
        customerEntity.Email = Console.ReadLine() ?? "";

        Console.WriteLine("Fyll i information för ärendet:");
        Console.Write("Beskrivning: ");
        var Description = Console.ReadLine() ?? "";

        Console.Write("Status: ");
        var Status = Console.ReadLine() ?? "";

        Console.Write("Kommentar: ");
        var Comment = Console.ReadLine() ?? "";

        Console.Write("Anläggning: ");
        var Facility = Console.ReadLine() ?? "";

        var incidentEntity = new IncidentEntity
        {
            Description = Description,
            Status = Status,
            Comment = Comment,
            Facility = Facility
        };

        customerEntity.Incidents.Add(incidentEntity);

        // Save customer and incident to the database
        await _customerService.SaveAsync(customerEntity, incidentEntity);
    }





    public async Task GetAllIncidentsAsync()
    {
        var customers = await _customerService.GetAllAsync();

        if (customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Kundnummer: {customer.Id}");
                Console.WriteLine($"Namn: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"E-postadress: {customer.Email}");

                foreach (var incident in customer.Incidents)
                {
                    Console.WriteLine("Incidenter:");
                    Console.WriteLine($"Incident ID: {incident.Id}");
                    Console.WriteLine($"Beskrivning: {incident.Description}");
                    Console.WriteLine($"Status: {incident.Status}");
                    Console.WriteLine($"Kommentar: {incident.Comment}");
                    Console.WriteLine("");
                }

                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("Inga kunder finns i databasen.");
            Console.WriteLine("");
        }
    }




    public async Task GetIncidentByEmailAsync()
    {
        Console.Write("Ange e-postadress på kunden: ");
        var email = Console.ReadLine();

        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine($"Ingen e-postadress angiven.");
            return;
        }

        // Hämta specifik kund och incidenter från databasen
        var customer = await _customerService.GetAsync(email);

        if (customer != null)
        {
            Console.WriteLine($"Kundnummer: {customer.Id}");
            Console.WriteLine($"Namn: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"E-postadress: {customer.Email}");

            if (customer.Incidents.Any())
            {
                Console.WriteLine("Incidenter:");
                foreach (var incident in customer.Incidents)
                {
                    Console.WriteLine($"Incident ID: {incident.Id}");
                    Console.WriteLine($"Beskrivning: {incident.Description}");
                    Console.WriteLine($"Status: {incident.Status}");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("Inga incidenter hittades för kunden.");
            }
        }
        else
        {
            Console.WriteLine($"Ingen kund med den angivna e-postadressen hittades.");
        }
    }


    public async Task UpdateIncidentStatusAsync()
    {
        Console.Write("Ange e-postadress på kunden: ");
        var email = Console.ReadLine();

        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine($"Ingen e-postadress angiven.");
            return;
        }

        var customer = await _customerService.GetAsync(email);

        if (customer == null)
        {
            Console.WriteLine($"Hittade inte någon kund med den angivna e-postadressen.");
            return;
        }

        Console.Write("Ange ID för incidenten du vill uppdatera: ");
        if (!int.TryParse(Console.ReadLine(), out int incidentId))
        {
            Console.WriteLine("Ogiltigt ID-format.");
            return;
        }

        var incident = customer.Incidents.FirstOrDefault(i => i.Id == incidentId);

        if (incident == null)
        {
            Console.WriteLine($"Ingen incident med ID {incidentId} hittades för kunden.");
            return;
        }

        Console.Write("Ny status för incidenten: ");
        string newStatus = Console.ReadLine();

        incident.Status = newStatus;

        // Update the status of the incident in the database
        await _customerService.UpdateAsync(customer);

        Console.WriteLine("Incidentens status har uppdaterats.");
    }




    public async Task DeleteAsync()
    {
        Console.Write("Ange e-postadress på kunden: ");
        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
            //delete specific customer from database
            await CustomerService.DeleteAsync(email);
        }
        else
        {
            Console.WriteLine($"Ingen e-postadressen angiven.");
            Console.WriteLine("");
        }

    }

}