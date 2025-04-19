using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class TipoCategoriaPersona
{
    public int IdCategoriaPersona { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
