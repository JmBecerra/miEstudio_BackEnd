using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Tarifa
    {
        public Tarifa()
        {
            Pagos = new HashSet<Pago>();
        }

        public int IdTarifa { get; set; }
        public int NumActividades { get; set; }
        public decimal Precio { get; set; }
        public bool? Activa { get; set; }
        public int IdPeriodo { get; set; }

        public virtual Periodo IdPeriodoNavigation { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
