﻿using System;
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

    public virtual DbSet<CantidadAniosConcesion> CantidadAniosConcesions { get; set; }

    public virtual DbSet<ContratoArchivo> ContratoArchivos { get; set; }

    public virtual DbSet<ContratoConcesion> ContratoConcesions { get; set; }

    public virtual DbSet<Cuota> Cuotas { get; set; }

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

    public virtual DbSet<SeccionesFosa> SeccionesFosas { get; set; }

    public virtual DbSet<SeccionesNicho> SeccionesNichos { get; set; }

    public virtual DbSet<SeccionesPanteone> SeccionesPanteones { get; set; }

    public virtual DbSet<TipoCategoriaPersona> TipoCategoriaPersonas { get; set; }

    public virtual DbSet<TipoNicho> TipoNichos { get; set; }

    public virtual DbSet<TipoNumeracionParcela> TipoNumeracionParcelas { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Tramite> Tramites { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActaDefuncion>(entity =>
        {
            entity.HasKey(e => e.IdActaDefuncion).HasName("PK__ActaDefu__4D9F36E2D1C6AB35");

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

        modelBuilder.Entity<CantidadAniosConcesion>(entity =>
        {
            entity.HasKey(e => e.IdCantidadAnios).HasName("PK__Cantidad__3B63B3F205101664");

            entity.ToTable("CantidadAniosConcesion");

            entity.Property(e => e.IdCantidadAnios).HasColumnName("idCantidadAnios");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
        });

        modelBuilder.Entity<ContratoArchivo>(entity =>
        {
            entity.HasKey(e => e.ContratoId).HasName("PK__Contrato__F7E1964E68FB4CEE");

            entity.ToTable("ContratoArchivo");

            entity.HasIndex(e => e.RowGuid, "UQ__Contrato__B03806EFA9708D83").IsUnique();

            entity.Property(e => e.ContratoId)
                .ValueGeneratedNever()
                .HasColumnName("contratoId");
            entity.Property(e => e.Contenido).HasColumnName("contenido");
            entity.Property(e => e.Extension)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("extension");
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(255)
                .HasColumnName("nombreArchivo");
            entity.Property(e => e.RowGuid).HasColumnName("rowGuid");

            entity.HasOne(d => d.Contrato).WithOne(p => p.ContratoArchivo)
                .HasForeignKey<ContratoArchivo>(d => d.ContratoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContratoA__contr__02084FDA");
        });

        modelBuilder.Entity<ContratoConcesion>(entity =>
        {
            entity.HasKey(e => e.IdContrato).HasName("PK__Contrato__91431FE1B0D29524");

            entity.ToTable("ContratoConcesion");

            entity.Property(e => e.IdContrato).HasColumnName("idContrato");
            entity.Property(e => e.CantidadAniosId).HasColumnName("cantidadAniosId");
            entity.Property(e => e.CantidadCuotas).HasColumnName("cantidadCuotas");
            entity.Property(e => e.CreacionContrato).HasColumnName("creacionContrato");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FosaDifuntoId).HasColumnName("fosaDifuntoId");
            entity.Property(e => e.NichoDifuntoId).HasColumnName("nichoDifuntoId");
            entity.Property(e => e.NumeroCuentaId).HasColumnName("numeroCuentaId");
            entity.Property(e => e.PanteonDifuntoId).HasColumnName("panteonDifuntoId");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Vencimiento).HasColumnName("vencimiento");

            entity.HasOne(d => d.CantidadAnios).WithMany(p => p.ContratoConcesions)
                .HasForeignKey(d => d.CantidadAniosId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContratoC__canti__75A278F5");

            entity.HasOne(d => d.CantidadCuotasNavigation).WithMany(p => p.ContratoConcesions)
                .HasForeignKey(d => d.CantidadCuotas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContratoC__canti__76969D2E");

            entity.HasOne(d => d.FosaDifunto).WithMany(p => p.ContratoConcesions)
                .HasForeignKey(d => d.FosaDifuntoId)
                .HasConstraintName("FK__ContratoC__fosaD__787EE5A0");

            entity.HasOne(d => d.NichoDifunto).WithMany(p => p.ContratoConcesions)
                .HasForeignKey(d => d.NichoDifuntoId)
                .HasConstraintName("FK__ContratoC__nicho__778AC167");

            entity.HasOne(d => d.NumeroCuenta).WithMany(p => p.ContratoConcesions)
                .HasForeignKey(d => d.NumeroCuentaId)
                .HasConstraintName("FK__ContratoC__numer__7A672E12");

            entity.HasOne(d => d.PanteonDifunto).WithMany(p => p.ContratoConcesions)
                .HasForeignKey(d => d.PanteonDifuntoId)
                .HasConstraintName("FK__ContratoC__pante__797309D9");
        });

        modelBuilder.Entity<Cuota>(entity =>
        {
            entity.HasKey(e => e.IdCuota).HasName("PK__cuotas__9BE53C1863462E58");

            entity.ToTable("cuotas");

            entity.Property(e => e.IdCuota).HasColumnName("idCuota");
            entity.Property(e => e.Cuota1).HasColumnName("cuota");
        });

        modelBuilder.Entity<Difunto>(entity =>
        {
            entity.HasKey(e => e.IdDifunto).HasName("PK__Difunto__7B797DB25AB968D9");

            entity.ToTable("Difunto");

            entity.Property(e => e.IdDifunto).HasColumnName("idDifunto");
            entity.Property(e => e.ActaDefuncion).HasColumnName("actaDefuncion");
            entity.Property(e => e.Apellido)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaDefuncion).HasColumnName("fechaDefuncion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.ActaDefuncionNavigation).WithMany(p => p.Difuntos)
                .HasForeignKey(d => d.ActaDefuncion)
                .HasConstraintName("FK__Difunto__actaDef__5FB337D6");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Difuntos)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Difunto__estado__60A75C0F");
        });

        modelBuilder.Entity<EstadoDifunto>(entity =>
        {
            entity.HasKey(e => e.IdEstadoDifunto).HasName("PK__EstadoDi__21F9C2A84F4AE73D");

            entity.ToTable("EstadoDifunto");

            entity.Property(e => e.IdEstadoDifunto).HasColumnName("idEstadoDifunto");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Fosa>(entity =>
        {
            entity.HasKey(e => e.IdFosa).HasName("PK__Fosas__69D90C2C3692C82C");

            entity.Property(e => e.IdFosa).HasColumnName("idFosa");
            entity.Property(e => e.Difuntos).HasColumnName("difuntos");
            entity.Property(e => e.NroFosa).HasColumnName("nroFosa");
            entity.Property(e => e.Seccion).HasColumnName("seccion");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.SeccionNavigation).WithMany(p => p.FosasNavigation)
                .HasForeignKey(d => d.Seccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fosas__seccion__5812160E");
        });

        modelBuilder.Entity<FosasDifunto>(entity =>
        {
            entity.HasKey(e => e.IdFosasDifuntos).HasName("PK__FosasDif__30C74E8F383BB6DD");

            entity.Property(e => e.IdFosasDifuntos).HasColumnName("idFosasDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("empresa");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.FosaId).HasColumnName("fosaId");
            entity.Property(e => e.Usuario).HasColumnName("usuario");
            entity.Property(e => e.Visibilidad)
                .HasDefaultValue(true)
                .HasColumnName("visibilidad");

            entity.HasOne(d => d.Difunto).WithMany(p => p.FosasDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FosasDifu__difun__693CA210");

            entity.HasOne(d => d.Fosa).WithMany(p => p.FosasDifuntos)
                .HasForeignKey(d => d.FosaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FosasDifu__fosaI__6A30C649");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.FosasDifuntos)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK_FosasDifuntos_Usuarios");
        });

        modelBuilder.Entity<Nicho>(entity =>
        {
            entity.HasKey(e => e.IdNicho).HasName("PK__Nicho__98EA600F3787F0F2");

            entity.ToTable("Nicho");

            entity.Property(e => e.IdNicho).HasColumnName("idNicho");
            entity.Property(e => e.Difuntos).HasColumnName("difuntos");
            entity.Property(e => e.NroFila).HasColumnName("nroFila");
            entity.Property(e => e.NroNicho).HasColumnName("nroNicho");
            entity.Property(e => e.Seccion).HasColumnName("seccion");
            entity.Property(e => e.TipoNicho).HasColumnName("tipoNicho");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.SeccionNavigation).WithMany(p => p.NichosNavigation)
                .HasForeignKey(d => d.Seccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nicho__seccion__5441852A");

            entity.HasOne(d => d.TipoNichoNavigation).WithMany(p => p.Nichos)
                .HasForeignKey(d => d.TipoNicho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nicho__tipoNicho__5535A963");
        });

        modelBuilder.Entity<NichosDifunto>(entity =>
        {
            entity.HasKey(e => e.IdNichosDifuntos).HasName("PK__NichosDi__BF8852B5A5EA3BB0");

            entity.Property(e => e.IdNichosDifuntos).HasColumnName("idNichosDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("empresa");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.NichoId).HasColumnName("nichoId");
            entity.Property(e => e.Usuario).HasColumnName("usuario");
            entity.Property(e => e.Visibilidad)
                .HasDefaultValue(true)
                .HasColumnName("visibilidad");

            entity.HasOne(d => d.Difunto).WithMany(p => p.NichosDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NichosDif__difun__6477ECF3");

            entity.HasOne(d => d.Nicho).WithMany(p => p.NichosDifuntos)
                .HasForeignKey(d => d.NichoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NichosDif__nicho__656C112C");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.NichosDifuntos)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK_NichosDifuntos_Usuarios");
        });

        modelBuilder.Entity<NumeroCuentum>(entity =>
        {
            entity.HasKey(e => e.IdNumeroCuenta).HasName("PK__NumeroCu__821E373FF9197029");

            entity.HasIndex(e => e.Numero, "UQ__NumeroCu__FC77F2116FC5835A").IsUnique();

            entity.Property(e => e.IdNumeroCuenta)
                .ValueGeneratedNever()
                .HasColumnName("idNumeroCuenta");
            entity.Property(e => e.Numero).HasColumnName("numero");
        });

        modelBuilder.Entity<PanteonDifunto>(entity =>
        {
            entity.HasKey(e => e.IdPanteonDifuntos).HasName("PK__PanteonD__CC11F36C236AED46");

            entity.Property(e => e.IdPanteonDifuntos).HasColumnName("idPanteonDifuntos");
            entity.Property(e => e.DifuntoId).HasColumnName("difuntoId");
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("empresa");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.PanteonId).HasColumnName("panteonId");
            entity.Property(e => e.Usuario).HasColumnName("usuario");
            entity.Property(e => e.Visibilidad)
                .HasDefaultValue(true)
                .HasColumnName("visibilidad");

            entity.HasOne(d => d.Difunto).WithMany(p => p.PanteonDifuntos)
                .HasForeignKey(d => d.DifuntoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PanteonDi__difun__6E01572D");

            entity.HasOne(d => d.Panteon).WithMany(p => p.PanteonDifuntos)
                .HasForeignKey(d => d.PanteonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PanteonDi__pante__6EF57B66");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.PanteonDifuntos)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK_PanteonDifuntos_Usuarios");
        });

        modelBuilder.Entity<Panteone>(entity =>
        {
            entity.HasKey(e => e.IdPanteon).HasName("PK__Panteone__542CF3B9D12A70C5");

            entity.Property(e => e.IdPanteon).HasColumnName("idPanteon");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Difuntos).HasColumnName("difuntos");
            entity.Property(e => e.IdSeccionPanteon).HasColumnName("idSeccionPanteon");
            entity.Property(e => e.NroLote).HasColumnName("nroLote");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

            entity.HasOne(d => d.IdSeccionPanteonNavigation).WithMany(p => p.PanteonesNavigation)
                .HasForeignKey(d => d.IdSeccionPanteon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Panteones__idSec__5AEE82B9");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__A4788141CED344DE");

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
                .HasConstraintName("FK__Personas__catego__4D94879B");

            entity.HasMany(d => d.Contratos).WithMany(p => p.Personas)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonaContrato",
                    r => r.HasOne<ContratoConcesion>().WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PersonaCo__contr__7E37BEF6"),
                    l => l.HasOne<Persona>().WithMany()
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PersonaCo__perso__7D439ABD"),
                    j =>
                    {
                        j.HasKey("PersonaId", "ContratoId").HasName("PK__PersonaC__ABC941B163022284");
                        j.ToTable("PersonaContrato");
                        j.IndexerProperty<int>("PersonaId").HasColumnName("personaId");
                        j.IndexerProperty<int>("ContratoId").HasColumnName("contratoId");
                    });
        });

        modelBuilder.Entity<SeccionesFosa>(entity =>
        {
            entity.HasKey(e => e.IdSeccionFosa).HasName("PK__Seccione__DD2A3E3C893C986B");

            entity.HasIndex(e => e.Nombre, "UQ__Seccione__72AFBCC66DA7D083").IsUnique();

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
            entity.HasKey(e => e.IdSeccionNicho).HasName("PK__Seccione__CC35CD4756865B31");

            entity.HasIndex(e => e.Nombre, "UQ__Seccione__72AFBCC625FEC69A").IsUnique();

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
                .HasConstraintName("FK__Secciones__tipoN__48CFD27E");
        });

        modelBuilder.Entity<SeccionesPanteone>(entity =>
        {
            entity.HasKey(e => e.IdSeccionPanteones).HasName("PK__Seccione__C8579C1C0B552E5C");

            entity.Property(e => e.IdSeccionPanteones).HasColumnName("idSeccionPanteones");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Panteones).HasColumnName("panteones");
            entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");
        });

        modelBuilder.Entity<TipoCategoriaPersona>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaPersona).HasName("PK__TipoCate__4D5A2EF5CC6157F8");

            entity.ToTable("TipoCategoriaPersona");

            entity.Property(e => e.IdCategoriaPersona).HasColumnName("idCategoriaPersona");
            entity.Property(e => e.Tipo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TipoNicho>(entity =>
        {
            entity.HasKey(e => e.IdTipoNicho).HasName("PK__TipoNich__6E053CEC4D827D4E");

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
            entity.HasKey(e => e.IdTipoNumeracionParcela).HasName("PK__TipoNume__195C1E01607B1284");

            entity.ToTable("TipoNumeracionParcela");

            entity.Property(e => e.IdTipoNumeracionParcela).HasColumnName("idTipoNumeracionParcela");
            entity.Property(e => e.TipoNumeracion)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipoNumeracion");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("PK__TipoUsua__03006BFF3560F0E2");

            entity.ToTable("TipoUsuario");

            entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");
            entity.Property(e => e.TipoUsuario1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoUsuario");
        });

        modelBuilder.Entity<Tramite>(entity =>
        {
            entity.HasKey(e => e.IdTramite).HasName("PK__Tramites__5B864698C51F1C38");

            entity.Property(e => e.IdTramite).HasColumnName("idTramite");
            entity.Property(e => e.IdFosasDifuntosFk).HasColumnName("idFosasDifuntosFK");
            entity.Property(e => e.IdNichosDifuntosFk).HasColumnName("idNichosDifuntosFK");
            entity.Property(e => e.IdPanteonesDifuntos).HasColumnName("idPanteonesDifuntos");

            entity.HasOne(d => d.IdFosasDifuntosFkNavigation).WithMany(p => p.Tramites)
                .HasForeignKey(d => d.IdFosasDifuntosFk)
                .HasConstraintName("FK__Tramites__idFosa__245D67DE");

            entity.HasOne(d => d.IdNichosDifuntosFkNavigation).WithMany(p => p.Tramites)
                .HasForeignKey(d => d.IdNichosDifuntosFk)
                .HasConstraintName("FK__Tramites__idNich__236943A5");

            entity.HasOne(d => d.IdPanteonesDifuntosNavigation).WithMany(p => p.Tramites)
                .HasForeignKey(d => d.IdPanteonesDifuntos)
                .HasConstraintName("FK__Tramites__idPant__25518C17");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A646C1AD20");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__2A586E0B4598B90A").IsUnique();

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
                .HasConstraintName("FK__Usuarios__tipoUs__5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
