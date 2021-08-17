using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class MiestudioDBContext : DbContext
    {
        public MiestudioDBContext()
        {
        }

        public MiestudioDBContext(DbContextOptions<MiestudioDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividads { get; set; }
        public virtual DbSet<ActividadesUsuario> ActividadesUsuarios { get; set; }
        public virtual DbSet<Medicion> Medicions { get; set; }
        public virtual DbSet<MedicionUsuario> MedicionUsuarios { get; set; }
        public virtual DbSet<Multimedium> Multimedia { get; set; }
        public virtual DbSet<Nivel> Nivels { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Periodo> Periodos { get; set; }
        public virtual DbSet<Reserva> Reservas { get; set; }
        public virtual DbSet<Tarifa> Tarifas { get; set; }
        public virtual DbSet<Tipo> Tipos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=localhost;Database=miestudioDB;User Id=sa;Password=J13m13b13!;MultipleActiveResultSets=True;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad)
                    .HasName("PK__activida__DCD348834793509F");

                entity.ToTable("actividad");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.Activa)
                    .IsRequired()
                    .HasColumnName("activa")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Dia)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dia")
                    .IsFixedLength(true);

                entity.Property(e => e.Horario)
                    .HasColumnType("time(0)")
                    .HasColumnName("horario");

                entity.Property(e => e.IdNivel).HasColumnName("id_nivel");

                entity.Property(e => e.IdTipo).HasColumnName("id_tipo");

                entity.Property(e => e.Ocupacion).HasColumnName("ocupacion");

                entity.HasOne(d => d.IdNivelNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.IdNivel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__id_ni__4BAC3F29");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__id_ti__4AB81AF0");
            });

            modelBuilder.Entity<ActividadesUsuario>(entity =>
            {
                entity.HasKey(e => e.IdActsUsuario)
                    .HasName("PK__activida__3436CC49E95AB198");

                entity.ToTable("actividades_usuario");

                entity.Property(e => e.IdActsUsuario).HasColumnName("id_acts_usuario");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.ActividadesUsuarios)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__id_ac__4E88ABD4");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ActividadesUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__id_us__4F7CD00D");
            });

            modelBuilder.Entity<Medicion>(entity =>
            {
                entity.HasKey(e => e.IdMedicion)
                    .HasName("PK__medicion__1F7462584C3BA546");

                entity.ToTable("medicion");

                entity.Property(e => e.IdMedicion).HasColumnName("id_medicion");

                entity.Property(e => e.Abdomen)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("abdomen");

                entity.Property(e => e.Agua)
                    .HasColumnType("decimal(2, 1)")
                    .HasColumnName("agua");

                entity.Property(e => e.Altura)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("altura");

                entity.Property(e => e.Cintura)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("cintura");

                entity.Property(e => e.Grasa)
                    .HasColumnType("decimal(2, 1)")
                    .HasColumnName("grasa");

                entity.Property(e => e.Musculo)
                    .HasColumnType("decimal(2, 1)")
                    .HasColumnName("musculo");

                entity.Property(e => e.Peso)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("peso");
            });

            modelBuilder.Entity<MedicionUsuario>(entity =>
            {
                entity.HasKey(e => e.IdMedicionUsuario)
                    .HasName("PK__medicion__6DCE8B43F1816B7D");

                entity.ToTable("medicion_usuario");

                entity.Property(e => e.IdMedicionUsuario).HasColumnName("id_medicion_usuario");

                entity.Property(e => e.Fecha)
                    .HasPrecision(0)
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMedicion).HasColumnName("id_medicion");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdMedicionNavigation)
                    .WithMany(p => p.MedicionUsuarios)
                    .HasForeignKey(d => d.IdMedicion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__medicion___id_me__68487DD7");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.MedicionUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__medicion___id_us__693CA210");
            });

            modelBuilder.Entity<Multimedium>(entity =>
            {
                entity.HasKey(e => e.IdMultimedia)
                    .HasName("PK__multimed__89C12EF6304A7EF8");

                entity.ToTable("multimedia");

                entity.Property(e => e.IdMultimedia).HasColumnName("id_multimedia");

                entity.Property(e => e.Fecha)
                    .HasPrecision(0)
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("path")
                    .IsFixedLength(true);

                entity.Property(e => e.Visualizar).HasColumnName("visualizar");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Multimedia)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__multimedi__id_ac__5441852A");
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasKey(e => e.IdNivel)
                    .HasName("PK__nivel__9CAF1C538A2CBFFF");

                entity.ToTable("nivel");

                entity.Property(e => e.IdNivel).HasColumnName("id_nivel");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Denom)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("denom")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago)
                    .HasName("PK__pago__0941B074038AC7F2");

                entity.ToTable("pago");

                entity.Property(e => e.IdPago).HasColumnName("id_pago");

                entity.Property(e => e.FechaAct)
                    .HasPrecision(0)
                    .HasColumnName("fecha_act");

                entity.Property(e => e.FechaCobro)
                    .HasPrecision(0)
                    .HasColumnName("fecha_cobro");

                entity.Property(e => e.IdTarifa).HasColumnName("id_tarifa");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Metodo).HasColumnName("metodo");

                entity.Property(e => e.Pagado).HasColumnName("pagado");

                entity.HasOne(d => d.IdTarifaNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdTarifa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pago__id_tarifa__619B8048");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pago__id_usuario__628FA481");
            });

            modelBuilder.Entity<Periodo>(entity =>
            {
                entity.HasKey(e => e.IdPeriodo)
                    .HasName("PK__periodo__801188B78182C52D");

                entity.ToTable("periodo");

                entity.Property(e => e.IdPeriodo).HasColumnName("id_periodo");

                entity.Property(e => e.NombrePeriodo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre_periodo")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva)
                    .HasName("PK__reserva__423CBE5D6B76734D");

                entity.ToTable("reserva");

                entity.Property(e => e.IdReserva).HasColumnName("id_reserva");

                entity.Property(e => e.Fecha)
                    .HasPrecision(0)
                    .HasColumnName("fecha");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reserva__id_acti__571DF1D5");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reserva__id_usua__5812160E");
            });

            modelBuilder.Entity<Tarifa>(entity =>
            {
                entity.HasKey(e => e.IdTarifa)
                    .HasName("PK__tarifa__747D038960766674");

                entity.ToTable("tarifa");

                entity.Property(e => e.IdTarifa).HasColumnName("id_tarifa");

                entity.Property(e => e.Activa)
                    .IsRequired()
                    .HasColumnName("activa")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdPeriodo).HasColumnName("id_periodo");

                entity.Property(e => e.NumActividades).HasColumnName("num_actividades");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(6, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdPeriodoNavigation)
                    .WithMany(p => p.Tarifas)
                    .HasForeignKey(d => d.IdPeriodo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tarifa__id_perio__5DCAEF64");
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__tipo__CF901089F7516797");

                entity.ToTable("tipo");

                entity.Property(e => e.IdTipo).HasColumnName("id_tipo");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Denom)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("denom")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuario__4E3E04AD786B41D2");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.Email, "UQ__usuario__AB6E6164F8B196A7")
                    .IsUnique();

                entity.HasIndex(e => e.Dni, "UQ__usuario__D87608A7DCDC9964")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Alta)
                    .HasColumnName("alta")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Apellido1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("apellido_1")
                    .IsFixedLength(true);

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("apellido_2")
                    .IsFixedLength(true);

                entity.Property(e => e.Dni)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("dni")
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaAlta)
                    .HasPrecision(0)
                    .HasColumnName("fecha_alta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaMod)
                    .HasPrecision(0)
                    .HasColumnName("fecha_mod")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("telefono")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoUsuario).HasColumnName("tipo_usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
