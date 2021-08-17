using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Actividad
    {
        public Actividad()
        {
            ActividadesUsuarios = new HashSet<ActividadesUsuario>();
            Multimedia = new HashSet<Multimedium>();
            Reservas = new HashSet<Reserva>();
        }

        public int IdActividad { get; set; }
        public string Dia { get; set; }
        public TimeSpan Horario { get; set; }
        public int Ocupacion { get; set; }
        public bool? Activa { get; set; }
        public int IdTipo { get; set; }
        public int IdNivel { get; set; }

        public virtual Nivel IdNivelNavigation { get; set; }
        public virtual Tipo IdTipoNavigation { get; set; }
        public virtual ICollection<ActividadesUsuario> ActividadesUsuarios { get; set; }
        public virtual ICollection<Multimedium> Multimedia { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
