using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Periodo
    {
        public Periodo()
        {
            Tarifas = new HashSet<Tarifa>();
        }

        public int IdPeriodo { get; set; }
        public string NombrePeriodo { get; set; }

        public virtual ICollection<Tarifa> Tarifas { get; set; }
    }
}
