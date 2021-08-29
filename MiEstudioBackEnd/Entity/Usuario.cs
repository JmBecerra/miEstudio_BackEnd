using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Usuario
    {
        public Usuario()
        {
            ActividadesUsuarios = new HashSet<ActividadesUsuario>();
            MedicionUsuarios = new HashSet<MedicionUsuario>();
            Pagos = new HashSet<Pago>();
            Reservas = new HashSet<Reserva>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public short Alta { get; set; }
        public DateTime? FechaNac { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaMod { get; set; }
        public string Password { get; set; }
        public short TipoUsuario { get; set; }

        public virtual ICollection<ActividadesUsuario> ActividadesUsuarios { get; set; }
        public virtual ICollection<MedicionUsuario> MedicionUsuarios { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
