using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MiEstudioBackEnd.Entity
{
    public partial class miestudioDBContext : DbContext
    {
        public miestudioDBContext()
        {
        }

        public miestudioDBContext(DbContextOptions<miestudioDBContext> options)
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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=miestudioDB;User Id=sa;Password=J13m13b13!;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad)
                    .HasName("PK__activida__DCD34883C678F306");

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
                    .HasColumnName("dia");

                entity.Property(e => e.Horario)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("horario");

                entity.Property(e => e.IdNivel).HasColumnName("id_nivel");

                entity.Property(e => e.Ocupacion).HasColumnName("ocupacion");

                entity.Property(e => e.Tipo).HasColumnName("tipo");

                entity.HasOne(d => d.IdNivelNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.IdNivel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__id_ni__4D94879B");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__tipo__4CA06362");
            });

            modelBuilder.Entity<ActividadesUsuario>(entity =>
            {
                entity.HasKey(e => e.IdActsUsuario)
                    .HasName("PK__activida__3436CC49C19ABA13");

                entity.ToTable("actividades_usuario");

                entity.Property(e => e.IdActsUsuario).HasColumnName("id_acts_usuario");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.ActividadesUsuarios)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__id_ac__5070F446");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ActividadesUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__id_us__5165187F");
            });

            modelBuilder.Entity<Medicion>(entity =>
            {
                entity.HasKey(e => e.IdMedicion)
                    .HasName("PK__medicion__1F7462587ECDE1E0");

                entity.ToTable("medicion");

                entity.Property(e => e.IdMedicion).HasColumnName("id_medicion");

                entity.Property(e => e.Abdomen)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("abdomen");

                entity.Property(e => e.Agua)
                    .HasColumnType("decimal(3, 1)")
                    .HasColumnName("agua");

                entity.Property(e => e.Altura)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("altura");

                entity.Property(e => e.Cintura)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("cintura");

                entity.Property(e => e.Grasa)
                    .HasColumnType("decimal(3, 1)")
                    .HasColumnName("grasa");

                entity.Property(e => e.Musculo)
                    .HasColumnType("decimal(3, 1)")
                    .HasColumnName("musculo");

                entity.Property(e => e.Peso)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("peso");
            });

            modelBuilder.Entity<MedicionUsuario>(entity =>
            {
                entity.HasKey(e => e.IdMedicionUsuario)
                    .HasName("PK__medicion__6DCE8B436AA17AA2");

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
                    .HasConstraintName("FK__medicion___id_me__6A30C649");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.MedicionUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__medicion___id_us__6B24EA82");
            });

            modelBuilder.Entity<Multimedium>(entity =>
            {
                entity.HasKey(e => e.IdMultimedia)
                    .HasName("PK__multimed__89C12EF6BE6E8868");

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
                    .HasColumnName("nombre");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("path");

                entity.Property(e => e.Visualizar).HasColumnName("visualizar");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Multimedia)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__multimedi__id_ac__5629CD9C");
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasKey(e => e.IdNivel)
                    .HasName("PK__nivel__9CAF1C53EFDC80B4");

                entity.ToTable("nivel");

                entity.Property(e => e.IdNivel).HasColumnName("id_nivel");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Denom)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("denom");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago)
                    .HasName("PK__pago__0941B07423405013");

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
                    .HasConstraintName("FK__pago__id_tarifa__6383C8BA");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pago__id_usuario__6477ECF3");
            });

            modelBuilder.Entity<Periodo>(entity =>
            {
                entity.HasKey(e => e.IdPeriodo)
                    .HasName("PK__periodo__801188B77A956B64");

                entity.ToTable("periodo");

                entity.Property(e => e.IdPeriodo).HasColumnName("id_periodo");

                entity.Property(e => e.NombrePeriodo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre_periodo");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva)
                    .HasName("PK__reserva__423CBE5D202C3F00");

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
                    .HasConstraintName("FK__reserva__id_acti__59063A47");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reserva__id_usua__59FA5E80");
            });

            modelBuilder.Entity<Tarifa>(entity =>
            {
                entity.HasKey(e => e.IdTarifa)
                    .HasName("PK__tarifa__747D03898810E6B6");

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
                    .HasConstraintName("FK__tarifa__id_perio__5FB337D6");
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__tipo__CF9010899A1DC417");

                entity.ToTable("tipo");

                entity.Property(e => e.IdTipo).HasColumnName("id_tipo");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Denom)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("denom");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuario__4E3E04ADB3454A78");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.Email, "UQ__usuario__AB6E61647DD1C540")
                    .IsUnique();

                entity.HasIndex(e => e.Dni, "UQ__usuario__D87608A7171A1558")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Alta)
                    .HasColumnName("alta")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Apellido1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("apellido_1");

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("apellido_2");

                entity.Property(e => e.Dni)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("dni");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaAlta)
                    .HasPrecision(0)
                    .HasColumnName("fecha_alta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaMod)
                    .HasPrecision(0)
                    .HasColumnName("fecha_mod")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaNac)
                    .HasPrecision(0)
                    .HasColumnName("fecha_nac");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoUsuario).HasColumnName("tipo_usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
