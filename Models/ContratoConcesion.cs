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

    public int? NichoDifuntoId { get; set; }

    public int? FosaDifuntoId { get; set; }

    public int? PanteonDifuntoId { get; set; }

    public string? Descripcion { get; set; }

    public int? NumeroCuentaId { get; set; }

    public virtual CantidadAniosConcesion CantidadAnios { get; set; } = null!;

    public virtual Cuota CantidadCuotasNavigation { get; set; } = null!;

    public virtual ContratoArchivo? ContratoArchivo { get; set; }

    public virtual FosasDifunto? FosaDifunto { get; set; }

    public virtual NichosDifunto? NichoDifunto { get; set; }

    public virtual NumeroCuentum? NumeroCuenta { get; set; }

    public virtual PanteonDifunto? PanteonDifunto { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
