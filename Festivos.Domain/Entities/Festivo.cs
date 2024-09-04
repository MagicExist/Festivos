using System;
using System.Collections.Generic;

namespace Festivos.API.Models;

public partial class Festivo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Dia { get; set; }

    public int Mes { get; set; }

    public int DiasPascua { get; set; }

    public int IdTipo { get; set; }

    public virtual Tipo IdTipoNavigation { get; set; } = null!;
}
