using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class ContratoArchivo
{
    public int ContratoId { get; set; }

    public string? NombreArchivo { get; set; }

    public byte[]? Contenido { get; set; }

    public Guid RowGuid { get; set; }

    public string? Extension { get; set; }

    public virtual ContratoConcesion Contrato { get; set; } = null!;
}
