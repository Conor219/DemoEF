using System;
using System.Collections.Generic;

namespace DBFirst;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Puesto { get; set; } = null!;
}
