using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class MedicionUsuario
    {
        public int IdMedicionUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMedicion { get; set; }
        public int IdUsuario { get; set; }

        public virtual Medicion IdMedicionNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
