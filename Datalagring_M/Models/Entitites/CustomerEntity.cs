using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Datalagring_M.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
public class CustomerEntity
{
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    public List<IncidentEntity> Incidents { get; } = new List<IncidentEntity>();
}