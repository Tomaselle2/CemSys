using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class NumeroCuentum
{
    public int IdNumeroCuenta { get; set; }

    public int Numero { get; set; }

    public virtual ICollection<Fosa> Fosas { get; set; } = new List<Fosa>();

    public virtual ICollection<Nicho> Nichos { get; set; } = new List<Nicho>();

    public virtual ICollection<Panteone> Panteones { get; set; } = new List<Panteone>();
}
