using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Nivel
    {
        public Nivel()
        {
            Actividads = new HashSet<Actividad>();
        }

        public int IdNivel { get; set; }
        public string Nombre { get; set; }
        public string Denom { get; set; }
        public short Activo { get; set; }

        public virtual ICollection<Actividad> Actividads { get; set; }
    }
}
