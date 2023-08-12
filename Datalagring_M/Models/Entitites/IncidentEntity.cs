using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_M.Models.Entities;

public class IncidentEntity
{
    public int Id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Comment { get; set; } = null!;

    [StringLength(50)]
    public string Facility { get; set; } = null!;
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; }
}
