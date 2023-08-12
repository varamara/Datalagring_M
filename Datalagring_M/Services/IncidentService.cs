using Datalagring_M.Contexts;
using Datalagring_M.Models.Entities;
using Datalagring_M.Models;

namespace Datalagring_M.Services
{
    public class IncidentService
    {
        private static DataContext _context = new DataContext();

        public static async Task SaveAsync(IncidentEntity incident)
        {
            var _incidentEntity = new IncidentEntity
            {
                Description = incident.Description,
                Status = incident.Status,
                Comment = incident.Comment,
                Facility = incident.Facility,

            };

            _context.Add(_incidentEntity);
            await _context.SaveChangesAsync();
        }


        public static async Task UpdateIncidentStatusAsync(int id, string newStatus)
        {
            var _context = new DataContext();

            // Hämta incidenten från databasen med hjälp av id
            var incident = await _context.Incidents.FindAsync(id);

            // Om incidenten inte hittas, kasta ett undantag
            if (incident == null)
            {
                throw new ArgumentException($"Ingen incident hittades med ID {id}");
            }

            // Uppdatera incidentens status
            incident.Status = newStatus;

            // Spara ändringar i databasen
            await _context.SaveChangesAsync();
        }


        

    }
}
