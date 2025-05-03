using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class ContratoConcesion
{
    public int IdContrato { get; set; }

    public decimal Precio { get; set; }

    public DateOnly CreacionContrato { get; set; }

    public DateOnly Vencimiento { get; set; }

    public int CantidadAniosId { get; set; }

    public int CantidadCuotas { get; set; }

    public int? NichoId { get; set; }

    public int? FosaId { get; set; }

    public int? PanteonId { get; set; }

    public virtual CantidadAniosConcesion CantidadAnios { get; set; } = null!;

    public virtual Cuota CantidadCuotasNavigation { get; set; } = null!;

    public virtual ContratoArchivo? ContratoArchivo { get; set; }

    public virtual Fosa? Fosa { get; set; }

    public virtual Nicho? Nicho { get; set; }

    public virtual Panteone? Panteon { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
