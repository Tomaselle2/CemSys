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

<<<<<<< HEAD
   
=======
  
>>>>>>> eed940fd7fa5c7e3d3c29209e7fcf3e8b1ff5c71

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActaDefuncion>(entity =>
        {
            entity.HasKey(e => e.IdActaDefuncion).HasName("PK__ActaDefu__4D9F36E28ED5D257");

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
            entity.HasKey(e => e.IdDifunto).HasName("PK__Difunto__7B797DB28E3B9F9E");

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
                .HasConstraintName("FK__Difunto__actaDef__6477ECF3");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Difuntos)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Difunto__estado__656C112C");
        });

        modelBuilder.Entity<EstadoDifunto>(entity =>
        {
            entity.HasKey(e => e.IdEstadoDifunto).HasName("PK__EstadoDi__21F9C2A8B6F611CD");

            entity.ToTable("EstadoDifunto");

            entity.Property(e => e.IdEstadoDifunto).HasColumnName("idEstadoDifunto");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Fosa>(entity =>
        {
            entity.HasKey(e => e.IdFosa).HasName("PK__Fosas__69D90C2CC95A2879");

            entity.Property(e => e.IdFosa).HasColumnName("idFosa");
            entity.Property(e => e.Difuntos).HasColumnName("difuntos");
            entity.Property(e => e.NroCuenta).HasColumnName("nroCuenta");
            entity.Property(e => e.NroFosa).HasColumnName("nroFosa");
            entity.Property(e => e.Seccion).HasColumnName("seccion");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.NroCuentaNavigation).WithMany(p => p.Fosas)
                .HasForeignKey(d => d.NroCuenta)
                .HasConstraintName("FK__Fosas__nroCuenta__4F7CD00D");

            entity.HasOne(d => d.SeccionNavigation).WithMany(p => p.FosasNavigation)
                .HasForeignKey(d => d.Seccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fosas__seccion__4E88ABD4");
        });

        modelBuilder.Entity<FosasDifunto>(entity =>
        {
            entity.HasKey(e => e.IdFosasDifuntos).HasName("PK__FosasDif__30C74E8F682AF9DF");

            entity.Property(e => e.IdFosasDifuntos).HasColumnName("idFosasDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.FosaId).HasColumnName("fosaId");

            entity.HasOne(d => d.Difunto).WithMany(p => p.FosasDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FosasDifu__difun__6C190EBB");

            entity.HasOne(d => d.Fosa).WithMany(p => p.FosasDifuntos)
                .HasForeignKey(d => d.FosaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FosasDifu__fosaI__6D0D32F4");
        });

        modelBuilder.Entity<Nicho>(entity =>
        {
            entity.HasKey(e => e.IdNicho).HasName("PK__Nicho__98EA600F7AB9DDA7");

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
                .HasConstraintName("FK__Nicho__nroCuenta__4AB81AF0");

            entity.HasOne(d => d.SeccionNavigation).WithMany(p => p.NichosNavigation)
                .HasForeignKey(d => d.Seccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nicho__seccion__49C3F6B7");

            entity.HasOne(d => d.TipoNichoNavigation).WithMany(p => p.Nichos)
                .HasForeignKey(d => d.TipoNicho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nicho__tipoNicho__4BAC3F29");
        });

        modelBuilder.Entity<NichosDifunto>(entity =>
        {
            entity.HasKey(e => e.IdNichosDifuntos).HasName("PK__NichosDi__BF8852B5B8B833F2");

            entity.Property(e => e.IdNichosDifuntos).HasColumnName("idNichosDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.NichoId).HasColumnName("nichoId");

            entity.HasOne(d => d.Difunto).WithMany(p => p.NichosDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NichosDif__difun__68487DD7");

            entity.HasOne(d => d.Nicho).WithMany(p => p.NichosDifuntos)
                .HasForeignKey(d => d.NichoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NichosDif__nicho__693CA210");
        });

        modelBuilder.Entity<NumeroCuentum>(entity =>
        {
            entity.HasKey(e => e.IdNumeroCuenta).HasName("PK__NumeroCu__821E373F7B2EF64D");

            entity.HasIndex(e => e.Numero, "UQ__NumeroCu__FC77F211B5204781").IsUnique();

            entity.Property(e => e.IdNumeroCuenta)
                .ValueGeneratedNever()
                .HasColumnName("idNumeroCuenta");
            entity.Property(e => e.Numero).HasColumnName("numero");
        });

        modelBuilder.Entity<PanteonDifunto>(entity =>
        {
            entity.HasKey(e => e.IdPanteonDifuntos).HasName("PK__PanteonD__CC11F36C7CF707DD");

            entity.Property(e => e.IdPanteonDifuntos).HasColumnName("idPanteonDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.PanteonId).HasColumnName("panteonId");

            entity.HasOne(d => d.Difunto).WithMany(p => p.PanteonDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PanteonDi__difun__6FE99F9F");

            entity.HasOne(d => d.Panteon).WithMany(p => p.PanteonDifuntos)
                .HasForeignKey(d => d.PanteonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PanteonDi__pante__70DDC3D8");
        });

        modelBuilder.Entity<Panteone>(entity =>
        {
            entity.HasKey(e => e.IdPanteon).HasName("PK__Panteone__542CF3B95F9AFC7D");

            entity.Property(e => e.IdPanteon).HasColumnName("idPanteon");
            entity.Property(e => e.Difuntos).HasColumnName("difuntos");
            entity.Property(e => e.NroCuenta).HasColumnName("nroCuenta");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.NroCuentaNavigation).WithMany(p => p.Panteones)
                .HasForeignKey(d => d.NroCuenta)
                .HasConstraintName("FK__Panteones__nroCu__52593CB8");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__A478814105CB3312");

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
            entity.HasKey(e => e.IdPersonaFosas).HasName("PK__Personas__9D39D5EAC554F2E1");

            entity.Property(e => e.IdPersonaFosas).HasColumnName("idPersonaFosas");
            entity.Property(e => e.FosaId).HasColumnName("fosaId");
            entity.Property(e => e.PersonalId).HasColumnName("personalId");

            entity.HasOne(d => d.Fosa).WithMany(p => p.PersonasFosas)
                .HasForeignKey(d => d.FosaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasF__fosaI__59FA5E80");

            entity.HasOne(d => d.Personal).WithMany(p => p.PersonasFosas)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasF__perso__59063A47");
        });

        modelBuilder.Entity<PersonasNicho>(entity =>
        {
            entity.HasKey(e => e.IdPersonaNicho).HasName("PK__Personas__AE19DC2768729F99");

            entity.Property(e => e.IdPersonaNicho).HasColumnName("idPersonaNicho");
            entity.Property(e => e.NichoId).HasColumnName("nichoId");
            entity.Property(e => e.PersonalId).HasColumnName("personalId");

            entity.HasOne(d => d.Nicho).WithMany(p => p.PersonasNichos)
                .HasForeignKey(d => d.NichoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasN__nicho__5629CD9C");

            entity.HasOne(d => d.Personal).WithMany(p => p.PersonasNichos)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasN__perso__5535A963");
        });

        modelBuilder.Entity<PersonasPanteone>(entity =>
        {
            entity.HasKey(e => e.IdPersonaFosas).HasName("PK__Personas__9D39D5EA36A5D619");

            entity.Property(e => e.IdPersonaFosas).HasColumnName("idPersonaFosas");
            entity.Property(e => e.PanteonSeId).HasColumnName("panteonSeId");
            entity.Property(e => e.PersonalId).HasColumnName("personalId");

            entity.HasOne(d => d.PanteonSe).WithMany(p => p.PersonasPanteones)
                .HasForeignKey(d => d.PanteonSeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasP__pante__5DCAEF64");

            entity.HasOne(d => d.Personal).WithMany(p => p.PersonasPanteones)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonasP__perso__5CD6CB2B");
        });

        modelBuilder.Entity<SeccionesFosa>(entity =>
        {
            entity.HasKey(e => e.IdSeccionFosa).HasName("PK__Seccione__DD2A3E3C067DBED0");

            entity.HasIndex(e => e.Nombre, "UQ__Seccione__72AFBCC6F00336E8").IsUnique();

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
            entity.HasKey(e => e.IdSeccionNicho).HasName("PK__Seccione__CC35CD47D0DC4DC0");

            entity.HasIndex(e => e.Nombre, "UQ__Seccione__72AFBCC62BC4AEA3").IsUnique();

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
                .HasConstraintName("FK__Secciones__tipoN__4222D4EF");
        });

        modelBuilder.Entity<TipoCategoriaPersona>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaPersona).HasName("PK__TipoCate__4D5A2EF5C21F8A83");

            entity.ToTable("TipoCategoriaPersona");

            entity.Property(e => e.IdCategoriaPersona).HasColumnName("idCategoriaPersona");
            entity.Property(e => e.Tipo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TipoNicho>(entity =>
        {
            entity.HasKey(e => e.IdTipoNicho).HasName("PK__TipoNich__6E053CEC581B126F");

            entity.ToTable("TipoNicho");

            entity.Property(e => e.IdTipoNicho).HasColumnName("idTipoNicho");
            entity.Property(e => e.TipoNicho1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("tipoNicho");
        });

        modelBuilder.Entity<TipoNumeracionParcela>(entity =>
        {
            entity.HasKey(e => e.IdTipoNumeracionParcela).HasName("PK__TipoNume__195C1E01C2CE9DDF");

            entity.ToTable("TipoNumeracionParcela");

            entity.Property(e => e.IdTipoNumeracionParcela).HasColumnName("idTipoNumeracionParcela");
            entity.Property(e => e.TipoNumeracion)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipoNumeracion");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("PK__TipoUsua__03006BFF8C5C4033");

            entity.ToTable("TipoUsuario");

            entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");
            entity.Property(e => e.TipoUsuario1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoUsuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A6B5B1A1A0");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__2A586E0BCD547497").IsUnique();

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
                .HasConstraintName("FK__Usuarios__tipoUs__76969D2E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
