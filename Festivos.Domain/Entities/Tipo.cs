using System;
using System.Collections.Generic;

namespace Festivos.API.Models;

public partial class Tipo
{
    public int Id { get; set; }

    public string Tipo1 { get; set; } = null!;

    public virtual ICollection<Festivo> Festivos { get; set; } = new List<Festivo>();
}
