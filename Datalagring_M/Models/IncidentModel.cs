using Datalagring_M.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalagring_M.Models;

public class IncidentModel
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public string Facility { get; set; } = null!;

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; }
}
