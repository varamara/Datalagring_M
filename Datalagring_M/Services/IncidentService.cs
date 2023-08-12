using Datalagring_M.Contexts;
using Datalagring_M.Models.Entities;

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

            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                throw new ArgumentException($"Ingen incident hittades med ID {id}");
            }

            incident.Status = newStatus;

            await _context.SaveChangesAsync();
        }
    }
}
