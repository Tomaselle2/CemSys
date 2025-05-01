using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CemSys.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActaDefuncion> ActaDefuncions { get; set; }

    public virtual DbSet<Difunto> Difuntos { get; set; }

    public virtual DbSet<EstadoDifunto> EstadoDifuntos { get; set; }

    public virtual DbSet<Fosa> Fosas { get; set; }

    public virtual DbSet<FosasDifunto> FosasDifuntos { get; set; }

    public virtual DbSet<Nicho> Nichos { get; set; }

    public virtual DbSet<NichosDifunto> NichosDifuntos { get; set; }

    public virtual DbSet<NumeroCuentum> NumeroCuenta { get; set; }

    public virtual DbSet<PanteonDifunto> PanteonDifuntos { get; set; }

    public virtual DbSet<Panteone> Panteones { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<PersonasFosa> PersonasFosas { get; set; }

    public virtual DbSet<PersonasNicho> PersonasNichos { get; set; }

    public virtual DbSet<PersonasPanteone> PersonasPanteones { get; set; }

    public virtual DbSet<SeccionesFosa> SeccionesFosas { get; set; }

    public virtual DbSet<SeccionesNicho> SeccionesNichos { get; set; }

    public virtual DbSet<TipoCategoriaPersona> TipoCategoriaPersonas { get; set; }

    public virtual DbSet<TipoNicho> TipoNichos { get; set; }

    public virtual DbSet<TipoNumeracionParcela> TipoNumeracionParcelas { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActaDefuncion>(entity =>
        {
            entity.HasKey(e => e.IdActaDefuncion).HasName("PK__ActaDefu__4D9F36E21EA9A2A8");

            entity.ToTable("ActaDefuncion");

            entity.Property(e => e.IdActaDefuncion).HasColumnName("idActaDefuncion");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.NroActa).HasColumnName("nroActa");
            entity.Property(e => e.Serie)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("serie");
            entity.Property(e => e.Tomo).HasColumnName("tomo");
        });

        modelBuilder.Entity<Difunto>(entity =>
        {
            entity.HasKey(e => e.IdDifunto).HasName("PK__Difunto__7B797DB2481A3939");

            entity.ToTable("Difunto");

            entity.Property(e => e.IdDifunto).HasColumnName("idDifunto");
            entity.Property(e => e.ActaDefuncion).HasColumnName("actaDefuncion");
            entity.Property(e => e.Apellido)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaDefuncion).HasColumnName("fechaDefuncion");
            entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.ActaDefuncionNavigation).WithMany(p => p.Difuntos)
                .HasForeignKey(d => d.ActaDefuncion)
                .HasConstraintName("FK__Difunto__actaDef__628FA481");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Difuntos)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Difunto__estado__6383C8BA");
        });

        modelBuilder.Entity<EstadoDifunto>(entity =>
        {
            entity.HasKey(e => e.IdEstadoDifunto).HasName("PK__EstadoDi__21F9C2A87A938CA0");

            entity.ToTable("EstadoDifunto");

            entity.Property(e => e.IdEstadoDifunto).HasColumnName("idEstadoDifunto");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Fosa>(entity =>
        {
            entity.HasKey(e => e.IdFosa).HasName("PK__Fosas__69D90C2CAFB56D55");

            entity.Property(e => e.IdFosa).HasColumnName("idFosa");
            entity.Property(e => e.Difuntos).HasColumnName("difuntos");
            entity.Property(e => e.NroCuenta).HasColumnName("nroCuenta");
            entity.Property(e => e.NroFosa).HasColumnName("nroFosa");
            entity.Property(e => e.Seccion).HasColumnName("seccion");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.NroCuentaNavigation).WithMany(p => p.Fosas)
                .HasForeignKey(d => d.NroCuenta)
                .HasConstraintName("FK__Fosas__nroCuenta__4D94879B");

            entity.HasOne(d => d.SeccionNavigation).WithMany(p => p.FosasNavigation)
                .HasForeignKey(d => d.Seccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fosas__seccion__4CA06362");
        });

        modelBuilder.Entity<FosasDifunto>(entity =>
        {
            entity.HasKey(e => e.IdFosasDifuntos).HasName("PK__FosasDif__30C74E8FCDBBE23A");

            entity.Property(e => e.IdFosasDifuntos).HasColumnName("idFosasDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.FosaId).HasColumnName("fosaId");

            entity.HasOne(d => d.Difunto).WithMany(p => p.FosasDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FosasDifu__difun__6A30C649");

            entity.HasOne(d => d.Fosa).WithMany(p => p.FosasDifuntos)
                .HasForeignKey(d => d.FosaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FosasDifu__fosaI__6B24EA82");
        });

        modelBuilder.Entity<Nicho>(entity =>
        {
            entity.HasKey(e => e.IdNicho).HasName("PK__Nicho__98EA600F7BE93121");

            entity.ToTable("Nicho");

            entity.Property(e => e.IdNicho).HasColumnName("idNicho");
            entity.Property(e => e.Difuntos).HasColumnName("difuntos");
            entity.Property(e => e.NroCuenta).HasColumnName("nroCuenta");
            entity.Property(e => e.NroFila).HasColumnName("nroFila");
            entity.Property(e => e.NroNicho).HasColumnName("nroNicho");
            entity.Property(e => e.Seccion).HasColumnName("seccion");
            entity.Property(e => e.TipoNicho).HasColumnName("tipoNicho");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.NroCuentaNavigation).WithMany(p => p.Nichos)
                .HasForeignKey(d => d.NroCuenta)
                .HasConstraintName("FK__Nicho__nroCuenta__48CFD27E");

            entity.HasOne(d => d.SeccionNavigation).WithMany(p => p.NichosNavigation)
                .HasForeignKey(d => d.Seccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nicho__seccion__47DBAE45");

            entity.HasOne(d => d.TipoNichoNavigation).WithMany(p => p.Nichos)
                .HasForeignKey(d => d.TipoNicho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nicho__tipoNicho__49C3F6B7");
        });

        modelBuilder.Entity<NichosDifunto>(entity =>
        {
            entity.HasKey(e => e.IdNichosDifuntos).HasName("PK__NichosDi__BF8852B52DB54324");

            entity.Property(e => e.IdNichosDifuntos).HasColumnName("idNichosDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.NichoId).HasColumnName("nichoId");

            entity.HasOne(d => d.Difunto).WithMany(p => p.NichosDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NichosDif__difun__66603565");

            entity.HasOne(d => d.Nicho).WithMany(p => p.NichosDifuntos)
                .HasForeignKey(d => d.NichoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NichosDif__nicho__6754599E");
        });

        modelBuilder.Entity<NumeroCuentum>(entity =>
        {
            entity.HasKey(e => e.IdNumeroCuenta).HasName("PK__NumeroCu__821E373FFA0FDB1D");

            entity.HasIndex(e => e.Numero, "UQ__NumeroCu__FC77F211EB87E158").IsUnique();

            entity.Property(e => e.IdNumeroCuenta)
                .ValueGeneratedNever()
                .HasColumnName("idNumeroCuenta");
            entity.Property(e => e.Numero).HasColumnName("numero");
        });

        modelBuilder.Entity<PanteonDifunto>(entity =>
        {
            entity.HasKey(e => e.IdPanteonDifuntos).HasName("PK__PanteonD__CC11F36CF2887D65");

            entity.Property(e => e.IdPanteonDifuntos).HasColumnName("idPanteonDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.PanteonId).HasColumnName("panteonId");

            entity.HasOne(d => d.Difunto).WithMany(p => p.PanteonDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PanteonDi__difun__6E01572D");

            entity.HasOne(d => d.Panteon).WithMany(p => p.PanteonDifuntos)
                .HasForeignKey(d => d.PanteonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PanteonDi__pante__6EF57B66");
        });

        modelBuilder.Entity<Panteone>(entity =>
        {
            entity.HasKey(e => e.IdPanteon).HasName("PK__Panteone__542CF3B96959852A");

            entity.Property(e => e.IdPanteon).HasColumnName("idPanteon");
            entity.Property(e => e.Difuntos).HasColumnName("difuntos");
            entity.Property(e => e.NroCuenta).HasColumnName("nroCuenta");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.NroCuentaNavigation).WithMany(p => p.Panteones)
                .HasForeignKey(d => d.NroCuenta)
                .HasConstraintName("FK__Panteones__nroCu__5070F446");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__A47881410D4EEFE9");

            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.Apellido)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.CategoriaPersona).HasColumnName("categoriaPersona");
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Domicilio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("domicilio");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.CategoriaPersonaNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.CategoriaPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Personas__catego__398D8EEE");
        });

        modelBuilder.Entity<PersonasFosa>(entity =>
        {
            entity.HasKey(e => e.IdPersonaFosas).HasName("PK__Personas__9D39D5EA161AF7A0");

            entity.Property(e => e.IdPersonaFosas).HasColumnName("idPersonaFosas");
            entity.Property(e => e.FosaId).HasColumnName("fosaId");
            entity.Property(e => e.PersonalId).HasColumnName("personalId");

            entity.HasOne(d => d.Fosa).WithMany(p => p.PersonasFosas)
                .HasForeignKey(d => d.FosaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasF__fosaI__5812160E");

            entity.HasOne(d => d.Personal).WithMany(p => p.PersonasFosas)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasF__perso__571DF1D5");
        });

        modelBuilder.Entity<PersonasNicho>(entity =>
        {
            entity.HasKey(e => e.IdPersonaNicho).HasName("PK__Personas__AE19DC27D902C915");

            entity.Property(e => e.IdPersonaNicho).HasColumnName("idPersonaNicho");
            entity.Property(e => e.NichoId).HasColumnName("nichoId");
            entity.Property(e => e.PersonalId).HasColumnName("personalId");

            entity.HasOne(d => d.Nicho).WithMany(p => p.PersonasNichos)
                .HasForeignKey(d => d.NichoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasN__nicho__5441852A");

            entity.HasOne(d => d.Personal).WithMany(p => p.PersonasNichos)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasN__perso__534D60F1");
        });

        modelBuilder.Entity<PersonasPanteone>(entity =>
        {
            entity.HasKey(e => e.IdPersonaFosas).HasName("PK__Personas__9D39D5EAB943EEBD");

            entity.Property(e => e.IdPersonaFosas).HasColumnName("idPersonaFosas");
            entity.Property(e => e.PanteonSeId).HasColumnName("panteonSeId");
            entity.Property(e => e.PersonalId).HasColumnName("personalId");

            entity.HasOne(d => d.PanteonSe).WithMany(p => p.PersonasPanteones)
                .HasForeignKey(d => d.PanteonSeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasP__pante__5BE2A6F2");

            entity.HasOne(d => d.Personal).WithMany(p => p.PersonasPanteones)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasP__perso__5AEE82B9");
        });

        modelBuilder.Entity<SeccionesFosa>(entity =>
        {
            entity.HasKey(e => e.IdSeccionFosa).HasName("PK__Seccione__DD2A3E3CE2BD29CD");

            entity.HasIndex(e => e.Nombre, "UQ_SeccionesFosas_Nombre").IsUnique();

            entity.Property(e => e.IdSeccionFosa).HasColumnName("idSeccionFosa");
            entity.Property(e => e.Fosas).HasColumnName("fosas");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");
        });

        modelBuilder.Entity<SeccionesNicho>(entity =>
        {
            entity.HasKey(e => e.IdSeccionNicho).HasName("PK__Seccione__CC35CD47CC6804EE");

            entity.HasIndex(e => e.Nombre, "UQ_SeccionesNichos_Nombre").IsUnique();

            entity.Property(e => e.IdSeccionNicho).HasColumnName("idSeccionNicho");
            entity.Property(e => e.Filas).HasColumnName("filas");
            entity.Property(e => e.Nichos).HasColumnName("nichos");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoNumeracion).HasColumnName("tipoNumeracion");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.TipoNumeracionNavigation).WithMany(p => p.SeccionesNichos)
                .HasForeignKey(d => d.TipoNumeracion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Secciones__tipoN__403A8C7D");
        });

        modelBuilder.Entity<TipoCategoriaPersona>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaPersona).HasName("PK__TipoCate__4D5A2EF5740F9DAB");

            entity.ToTable("TipoCategoriaPersona");

            entity.Property(e => e.IdCategoriaPersona).HasColumnName("idCategoriaPersona");
            entity.Property(e => e.Tipo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TipoNicho>(entity =>
        {
            entity.HasKey(e => e.IdTipoNicho).HasName("PK__TipoNich__6E053CEC5D7DFE8D");

            entity.ToTable("TipoNicho");

            entity.Property(e => e.IdTipoNicho).HasColumnName("idTipoNicho");
            entity.Property(e => e.PorDefecto).HasColumnName("porDefecto");
            entity.Property(e => e.TipoNicho1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("tipoNicho");
        });

        modelBuilder.Entity<TipoNumeracionParcela>(entity =>
        {
            entity.HasKey(e => e.IdTipoNumeracionParcela).HasName("PK__TipoNume__195C1E017EF0B656");

            entity.ToTable("TipoNumeracionParcela");

            entity.Property(e => e.IdTipoNumeracionParcela).HasColumnName("idTipoNumeracionParcela");
            entity.Property(e => e.TipoNumeracion)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipoNumeracion");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("PK__TipoUsua__03006BFF8B24A027");

            entity.ToTable("TipoUsuario");

            entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");
            entity.Property(e => e.TipoUsuario1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoUsuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A68D097DE8");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__2A586E0BB993F9E3").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoUsuario).HasColumnName("tipoUsuario");

            entity.HasOne(d => d.TipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__tipoUs__74AE54BC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
