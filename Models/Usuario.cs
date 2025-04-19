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

    public virtual TipoUsuario TipoUsuarioNavigation { get; set; } = null!;
}
