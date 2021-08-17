using System;
using System.Collections.Generic;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class Medicion
    {
        public Medicion()
        {
            MedicionUsuarios = new HashSet<MedicionUsuario>();
        }

        public int IdMedicion { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public decimal? Grasa { get; set; }
        public decimal? Musculo { get; set; }
        public decimal? Agua { get; set; }
        public decimal? Abdomen { get; set; }
        public decimal? Cintura { get; set; }

        public virtual ICollection<MedicionUsuario> MedicionUsuarios { get; set; }
    }
}
