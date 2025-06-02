using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Correo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public int TipoUsuario { get; set; }

    public virtual ICollection<FosasDifunto> FosasDifuntos { get; set; } = new List<FosasDifunto>();

    public virtual ICollection<NichosDifunto> NichosDifuntos { get; set; } = new List<NichosDifunto>();

    public virtual ICollection<PanteonDifunto> PanteonDifuntos { get; set; } = new List<PanteonDifunto>();

    public virtual TipoUsuario TipoUsuarioNavigation { get; set; } = null!;
}
