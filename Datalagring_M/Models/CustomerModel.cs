
using Datalagring_M.Models.Entities;

namespace Datalagring_M.Models;

public class CustomerModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public string Facility { get; set; } = null!;

    public List<IncidentEntity> Incidents { get; set; } = null!;
}
