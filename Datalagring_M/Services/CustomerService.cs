using Datalagring_M.Models.Entities;
using Datalagring_M.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_M.Services;

internal class CustomerService
{
    private static DataContext _context = new DataContext();

    public async Task SaveAsync(CustomerEntity customerEntity, IncidentEntity incidentEntity)
    {
        var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == customerEntity.FirstName && x.LastName == customerEntity.LastName && x.Email == customerEntity.Email);

        if (existingCustomer != null)
        {
            incidentEntity.CustomerId = existingCustomer.Id;
        }
        else
        {
            customerEntity.Incidents.Add(incidentEntity);
            _context.Customers.Add(customerEntity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<CustomerEntity>> GetAllAsync()
    {
        var customers = await _context.Customers
            .Include(c => c.Incidents)
            .ToListAsync();

        return customers;
    }


    public async Task<CustomerEntity> GetAsync(string email)
    {
        var customerEntity = await _context.Customers
            .Include(c => c.Incidents)
            .FirstOrDefaultAsync(c => c.Email == email);

        return customerEntity;
    }

    public async Task UpdateAsync(CustomerEntity customerEntity)
    {
        var existingCustomer = await _context.Customers
            .Include(c => c.Incidents)
            .FirstOrDefaultAsync(c => c.Id == customerEntity.Id);

        if (existingCustomer != null)
        {
            foreach (var incidentEntity in customerEntity.Incidents)
            {
                var existingIncident = existingCustomer.Incidents.FirstOrDefault(i => i.Id == incidentEntity.Id);
                if (existingIncident != null)
                {
                    existingIncident.Status = incidentEntity.Status;
                }
            }

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(string email)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}