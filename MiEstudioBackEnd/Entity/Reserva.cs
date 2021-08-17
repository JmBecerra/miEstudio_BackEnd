using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Reserva
    {
        public int IdReserva { get; set; }
        public DateTime Fecha { get; set; }
        public int IdActividad { get; set; }
        public int IdUsuario { get; set; }

        public virtual Actividad IdActividadNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
