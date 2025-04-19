using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class PersonasNicho
{
    public int IdPersonaNicho { get; set; }

    public int PersonalId { get; set; }

    public int NichoId { get; set; }

    public virtual Nicho Nicho { get; set; } = null!;

    public virtual Persona Personal { get; set; } = null!;
}
