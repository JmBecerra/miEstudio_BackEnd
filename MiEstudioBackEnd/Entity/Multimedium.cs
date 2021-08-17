using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Multimedium
    {
        public int IdMultimedia { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Path { get; set; }
        public bool Visualizar { get; set; }
        public int IdActividad { get; set; }

        public virtual Actividad IdActividadNavigation { get; set; }
    }
}
