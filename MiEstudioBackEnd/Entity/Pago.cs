using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Pago
    {
        public int IdPago { get; set; }
        public DateTime FechaAct { get; set; }
        public DateTime FechaCobro { get; set; }
        public bool Pagado { get; set; }
        public short Metodo { get; set; }
        public int IdUsuario { get; set; }
        public int IdTarifa { get; set; }

        public virtual Tarifa IdTarifaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
