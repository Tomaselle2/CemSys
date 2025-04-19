using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class TipoUsuario
{
    public int IdTipoUsuario { get; set; }

    public string TipoUsuario1 { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
