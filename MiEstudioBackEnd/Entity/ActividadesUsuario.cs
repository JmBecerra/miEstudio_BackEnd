using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class ActividadesUsuario
    {
        public int IdActsUsuario { get; set; }
        public int IdActividad { get; set; }
        public int IdUsuario { get; set; }

        public virtual Actividad IdActividadNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
