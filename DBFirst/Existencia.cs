using System;
using System.Collections.Generic;

namespace DBFirst;

public partial class Existencia
{
    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
