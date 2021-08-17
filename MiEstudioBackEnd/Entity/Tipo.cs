using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Tipo
    {
        public Tipo()
        {
            Actividads = new HashSet<Actividad>();
        }

        public int IdTipo { get; set; }
        public string Nombre { get; set; }
        public string Denom { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Actividad> Actividads { get; set; }
    }
}
