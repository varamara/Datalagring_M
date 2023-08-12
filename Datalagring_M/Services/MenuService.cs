
using Datalagring_M.Models.Entities;

namespace Datalagring_M.Services;

internal class MenuService
{
    private readonly CustomerService _customerService;

    public MenuService(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task CreateAsync()
    {
        var customerEntity = new CustomerEntity();

        Console.WriteLine("Fyll i information för ärendet:");
        Console.WriteLine(" -------------------------------");
        Console.Write("Förnamn: ");
        customerEntity.FirstName = Console.ReadLine() ?? "";

        Console.Write("Efternamn: ");
        customerEntity.LastName = Console.ReadLine() ?? "";

        Console.Write("E-postadress: ");
        customerEntity.Email = Console.ReadLine() ?? "";

        Console.WriteLine(" -------------------------------");

        Console.Write("Anläggning: ");
        var Facility = Console.ReadLine() ?? "";

        Console.Write("Beskrivning: ");
        var Description = Console.ReadLine() ?? "";

        Console.Write("Kommentar: ");
        var Comment = Console.ReadLine() ?? "";

        Console.Write("Status - pågående/I kö/avslutat: ");
        var Status = Console.ReadLine() ?? "";


        var incidentEntity = new IncidentEntity
        {
            Description = Description,
            Status = Status,
            Comment = Comment,
            Facility = Facility
        };

        customerEntity.Incidents.Add(incidentEntity);

        await _customerService.SaveAsync(customerEntity, incidentEntity);
    }

    public async Task AddIncidentToCustomerAsync()
    {
        Console.Write("Ange e-postadress på kunden: ");
        var email = Console.ReadLine();

        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine($"Ingen e-postadress angiven.");
            return;
        }

        var existingCustomer = await _customerService.GetAsync(email);

        if (existingCustomer != null)
        {
            Console.WriteLine("Fyll i information för ärendet:");
            Console.Write("Beskrivning: ");
            var description = Console.ReadLine() ?? "";

            Console.Write("Status: ");
            var status = Console.ReadLine() ?? "";

            Console.Write("Kommentar: ");
            var comment = Console.ReadLine() ?? "";

            Console.Write("Anläggning: ");
            var facility = Console.ReadLine() ?? "";

            var newIncident = new IncidentEntity
            {
                Description = description,
                Status = status,
                Comment = comment,
                Facility = facility
            };

            existingCustomer.Incidents.Add(newIncident);

            await _customerService.UpdateAsync(existingCustomer);

            Console.WriteLine("Ny incident har lagts till för kunden.");
        }
        else
        {
            Console.WriteLine($"Ingen kund med den angivna e-postadressen hittades.");
        }
    }

    public async Task GetAllIncidentsAsync()
    {
        var customers = await _customerService.GetAllAsync();

        if (customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("");
                Console.WriteLine("--------------------------");
                Console.WriteLine("");
                Console.WriteLine("Kundinformation:");
                Console.WriteLine("");
                Console.WriteLine($"Kund ID: {customer.Id}");
                Console.WriteLine($"Namn: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"E-postadress: {customer.Email}");
                Console.WriteLine(" ");

                foreach (var incident in customer.Incidents)
                {
                    Console.WriteLine("Incidentinformation:");
                    Console.WriteLine("");
                    Console.WriteLine($"Incident ID: {incident.Id}");
                    Console.WriteLine($"Gällande anläggning: {incident.Facility}");
                    Console.WriteLine($"Beskrivning: {incident.Description}");
                    Console.WriteLine($"Kommentar: {incident.Comment}");
                    Console.WriteLine($"Status: {incident.Status}");
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

        var customer = await _customerService.GetAsync(email);

        if (customer != null)
        {
            Console.WriteLine("Kundinformation:");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Kund ID: {customer.Id}");
            Console.WriteLine($"Namn: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"E-postadress: {customer.Email}");
            

            if (customer.Incidents.Any())
            {
                Console.WriteLine(" ");
                Console.WriteLine("Incidentinformation:");
                Console.WriteLine("--------------------");
                foreach (var incident in customer.Incidents)
                {
                    Console.WriteLine($"Incident ID: {incident.Id}");
                    Console.WriteLine($"Gällande anläggning: {incident.Facility}");
                    Console.WriteLine($"Beskrivning: {incident.Description}");
                    Console.WriteLine($"Kommentar: {incident.Comment}");
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

        Console.Write("Ny status för incidenten - pågående/I kö/avslutat: ");
        string newStatus = Console.ReadLine();

        incident.Status = newStatus;

        await _customerService.UpdateAsync(customer);

        Console.WriteLine($"Incidentens status har uppdaterats till {newStatus}.");
    }

    public async Task DeleteAsync()
    {
        Console.Write("Ange e-postadress på kunden: ");
        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
          
            await _customerService.DeleteAsync(email);
            Console.WriteLine("Angiven kund har raderats.");
        }
        else
        {
            Console.WriteLine($"Ingen e-postadressen angiven.");
            Console.WriteLine("");
        }
    }
}