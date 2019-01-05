using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace Eureka.Models
{
    public partial class EurekaContext : DbContext
    {
        public EurekaContext()
        {
        }

        public EurekaContext(DbContextOptions<EurekaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<CompraDetalle> CompraDetalles { get; set; }
        public virtual DbSet<CompraEstado> CompraEstados { get; set; }
        public virtual DbSet<EmpresaInfo> EmpresaInfo { get; set; }
        public virtual DbSet<Ente> Entes { get; set; }
        public virtual DbSet<Entrada> Entradas { get; set; }
        public virtual DbSet<EntradaDetalle> EntradaDetalles { get; set; }
        public virtual DbSet<EntradaEstado> EntradaEstados { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Existencia> Existencias { get; set; }
        public virtual DbSet<Familia> Familias { get; set; }
        public virtual DbSet<FormaPago> FormaPagos { get; set; }
        public virtual DbSet<Inventario> Inventarios { get; set; }
        public virtual DbSet<Perfil> Perfiles { get; set; }
        public virtual DbSet<Presentacion> Presentaciones { get; set; }
        public virtual DbSet<Recurso> Recursos { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<RolRecursos> RolRecursos { get; set; }
        public virtual DbSet<Salida> Salidas { get; set; }
        public virtual DbSet<SalidaDetalle> SalidaDetalles { get; set; }
        public virtual DbSet<SalidaEstado> SalidaEstados { get; set; }
        public virtual DbSet<Servicio> Servicios { get; set; }
        public virtual DbSet<ServicioEstandar> ServicioEstandares { get; set; }
        public virtual DbSet<Sexo> Sexos { get; set; }
        public virtual DbSet<TipoEnte> TipoEntes { get; set; }
        public virtual DbSet<TipoEntrada> TipoEntradas { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificaciones { get; set; }
        public virtual DbSet<TipoSalida> TipoSalidas { get; set; }
        public virtual DbSet<Um> Ums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LEX-PC\\PCLEX;Database=Eureka;User Id=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estado_Area");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nota)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasIndex(e => e.AreaId)
                    .HasName("IX_Compra_Area");

                entity.HasIndex(e => e.CompraEstadoId)
                    .HasName("IX_Compra_CompraEstado");

                entity.HasIndex(e => e.EnteId)
                    .HasName("IX_Compra_Ente");

                entity.HasIndex(e => e.EstadoId)
                    .HasName("IX_Compra_Estado");

                entity.HasIndex(e => e.FormaPagoId)
                    .HasName("IX_Compra_FormaPago");

                entity.Property(e => e.CreadoEl).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descuento).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ModificadoEl).HasColumnType("datetime");

                entity.Property(e => e.ModificadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observacion).HasMaxLength(500);

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_Area");

                entity.HasOne(d => d.CompraEstado)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.CompraEstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_CompraEstado");

                entity.HasOne(d => d.CreadoPorNavigation)
                    .WithMany(p => p.ComprasCreadas)
                    .HasForeignKey(d => d.CreadoPor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_PerfilCreado");

                entity.HasOne(d => d.Ente)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.EnteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_Ente");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estado_Compra");

                entity.HasOne(d => d.FormaPago)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.FormaPagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_FormaPago");

                entity.HasOne(d => d.ModificadoPorNavigation)
                    .WithMany(p => p.ComprasModificadas)
                    .HasForeignKey(d => d.ModificadoPor)
                    .HasConstraintName("FK_Compra_PerfilModificado");
            });

            modelBuilder.Entity<CompraDetalle>(entity =>
            {
                entity.HasIndex(e => e.CompraId)
                    .HasName("IX_CompraDetalle_Compra");

                entity.HasIndex(e => e.InventarioId)
                    .HasName("IX_CompraDetalle_Inventario");

                entity.HasIndex(e => e.NoFactura);

                entity.Property(e => e.Costo).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CostoPromedio).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Descuento).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.NoFactura)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NuevoPrecio).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Precio).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Compra)
                    .WithMany(p => p.CompraDetalles)
                    .HasForeignKey(d => d.CompraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompraDetalle_Compra");

                entity.HasOne(d => d.Inventarios)
                    .WithMany(p => p.CompraDetalles)
                    .HasForeignKey(d => d.InventarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompraDetalle_Inventario");
            });

            modelBuilder.Entity<CompraEstado>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpresaInfo>(entity =>
            {
                entity.Property(e => e.FormatoFecha)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'dd/mm/yyyy')");

                entity.Property(e => e.Logotipo)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ente>(entity =>
            {
                entity.HasIndex(e => e.CategoriaId)
                    .HasName("IX_Ente_Categoria");

                entity.HasIndex(e => e.EstadoId)
                    .HasName("IX_Ente_Estado");

                entity.HasIndex(e => e.NombreCompleto);

                entity.Property(e => e.Celular)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.CreadoEl).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditoMaximo).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Direccion).HasMaxLength(350);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Identificacion).HasMaxLength(25);

                entity.Property(e => e.ModificadoEl).HasColumnType("datetime");

                entity.Property(e => e.ModificadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Observacion).HasMaxLength(500);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Entes)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ente_Categoria");

                entity.HasOne(d => d.CreadoPorNavigation)
                    .WithMany(p => p.EntesCreados)
                    .HasForeignKey(d => d.CreadoPor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ente_PerfilCreado");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Entes)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ente_Estado");

                entity.HasOne(d => d.ModificadoPorNavigation)
                    .WithMany(p => p.EntesModificados)
                    .HasForeignKey(d => d.ModificadoPor)
                    .HasConstraintName("FK_Ente_PerfilModificado");

                entity.HasOne(d => d.Sexo)
                    .WithMany(p => p.Entes)
                    .HasForeignKey(d => d.SexoId)
                    .HasConstraintName("FK_Ente_Sexo");

                entity.HasOne(d => d.TipoEnte)
                    .WithMany(p => p.Entes)
                    .HasForeignKey(d => d.TipoEnteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ente_TipoEnte");

                entity.HasOne(d => d.TipoIdentificacion)
                    .WithMany(p => p.Entes)
                    .HasForeignKey(d => d.TipoIdentificacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ente_TipoIdentificacion");          
                
            });

            modelBuilder.Entity<Entrada>(entity =>
            {
                entity.HasIndex(e => e.AreaId)
                    .HasName("IX_Entrada_Area");

                entity.HasIndex(e => e.EnteId)
                    .HasName("IX_Entrada_Ente");

                entity.HasIndex(e => e.EntradaEstadoId)
                    .HasName("IX_Entrada_EntradaEstado");

                entity.HasIndex(e => e.EstadoId)
                    .HasName("IX_Entrada_Estado");

                entity.HasIndex(e => e.FormaPagoId)
                    .HasName("IX_Entrada_FormaPago");

                entity.HasIndex(e => e.TipoEntradaId)
                    .HasName("IX_Entrada_TipoEntrada");

                entity.Property(e => e.Abonado).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CreadoEl).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descuento).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ModificadoEl).HasColumnType("datetime");

                entity.Property(e => e.ModificadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observacion).HasMaxLength(500);

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Entradas)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entrada_Area");

                entity.HasOne(d => d.CreadoPorNavigation)
                    .WithMany(p => p.EntradasCreadas)
                    .HasForeignKey(d => d.CreadoPor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entrada_PerfilCreado");

                entity.HasOne(d => d.Ente)
                    .WithMany(p => p.Entradas)
                    .HasForeignKey(d => d.EnteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entrada_Ente");

                entity.HasOne(d => d.EntradaEstado)
                    .WithMany(p => p.Entradas)
                    .HasForeignKey(d => d.EntradaEstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entrada_EntradaEstado");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Entradas)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entrada_Estado");

                entity.HasOne(d => d.FormaPago)
                    .WithMany(p => p.Entradas)
                    .HasForeignKey(d => d.FormaPagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entrada_FormaPago");

                entity.HasOne(d => d.ModificadoPorNavigation)
                    .WithMany(p => p.EntradasModificadas)
                    .HasForeignKey(d => d.ModificadoPor)
                    .HasConstraintName("FK_Entrada_PerfilModificado");

                entity.HasOne(d => d.TipoEntrada)
                    .WithMany(p => p.Entradas)
                    .HasForeignKey(d => d.TipoEntradaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entrada_TipoEntrada");
            });

            modelBuilder.Entity<EntradaDetalle>(entity =>
            {
                entity.HasIndex(e => e.EntradaId)
                    .HasName("IX_EntradaDetalle_Entrada");

                entity.HasIndex(e => e.InventarioId)
                    .HasName("IX_EntradaDetalle_Inventario");

                entity.Property(e => e.Costo).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CostoPromedio).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Descuento).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Precio).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.PrecioAnterior).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.PrecioNuevo).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Entrada)
                    .WithMany(p => p.EntradaDetalles)
                    .HasForeignKey(d => d.EntradaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntradaDetalle_Entrada");

                entity.HasOne(d => d.Inventarios)
                    .WithMany(p => p.EntradaDetalles)
                    .HasForeignKey(d => d.InventarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntradaDetalle_Inventario");
            });

            modelBuilder.Entity<EntradaEstado>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Existencia>(entity =>
            {
                entity.HasKey(e => new { e.InventarioId, e.AreaId });

                entity.Property(e => e.CostoPromedio).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ReglaDescuento)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ReglaPrecio)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Existencias)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Existencia_Area");

                entity.HasOne(d => d.Inventario)
                    .WithMany(p => p.Existencias)
                    .HasForeignKey(d => d.InventarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Existencia_Inventario");
            });

            modelBuilder.Entity<Familia>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Familias)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Familia_Estado");
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.FormaPagos)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormaPago_Estado");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasIndex(e => e.EstadoId)
                    .HasName("IX_Inventario_Estado");

                entity.HasIndex(e => e.FamiliaId)
                    .HasName("IX_Inventario_Familia");

                entity.HasIndex(e => e.Nombre);

                entity.HasIndex(e => e.PresentacionId)
                    .HasName("IX_Inventario_Presentacion");

                entity.HasIndex(e => e.Um);

                entity.Property(e => e.CreadoEl).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion).HasMaxLength(350);

                entity.Property(e => e.ModificadoEl).HasColumnType("datetime");

                entity.Property(e => e.ModificadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.CreadoPorNavigation)
                    .WithMany(p => p.InventariosCreados)
                    .HasForeignKey(d => d.CreadoPor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventario_PerfilCreado");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventario_Estado");

                entity.HasOne(d => d.Familia)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.FamiliaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventario_Familia");

                entity.HasOne(d => d.ModificadoPorNavigation)
                    .WithMany(p => p.InventariosModificados)
                    .HasForeignKey(d => d.ModificadoPor)
                    .HasConstraintName("FK_Inventario_PerfilModificado");

                entity.HasOne(d => d.Presentacion)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.PresentacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventario_Presentacion");

                entity.HasOne(d => d.UmNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.Um)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventario_Um");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.HasIndex(e => e.AreaId)
                    .HasName("IX_Perfil_Area");

                entity.HasIndex(e => e.Correo)
                    .HasName("IX_PerfilCorreo")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UrlTemporal).HasMaxLength(500);

                entity.Property(e => e.UtcreadoEl)
                    .HasColumnName("UTCreadoEl")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Perfiles)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Perfil_Area");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Perfiles)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Perfil_Estado");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Perfiles)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Perfil_Rol");
            });

            modelBuilder.Entity<Presentacion>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Presentaciones)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Presentacion_Estado");
            });

            modelBuilder.Entity<Recurso>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Recursos)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recurso_Estado");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rol_Estado");
            });

            modelBuilder.Entity<RolRecursos>(entity =>
            {
                entity.HasKey(e => new { e.RolId, e.RecursoId });

                entity.HasOne(d => d.Recurso)
                    .WithMany(p => p.RolRecursos)
                    .HasForeignKey(d => d.RecursoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolRecursos_Recurso");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolRecursos)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolRecursos_Rol");
            });

            modelBuilder.Entity<Salida>(entity =>
            {
                entity.HasIndex(e => e.AreaId)
                    .HasName("IX_Salida_Area");

                entity.HasIndex(e => e.EnteId)
                    .HasName("IX_Salida_Ente");

                entity.HasIndex(e => e.EstadoId)
                    .HasName("IX_Salida_Estado");

                entity.HasIndex(e => e.FormaPagoId)
                    .HasName("IX_Salida_FormaPago");

                entity.HasIndex(e => e.SalidaEstadoId)
                    .HasName("IX_Salida_SalidaEstado");

                entity.HasIndex(e => e.TipoSalidaId)
                    .HasName("IX_Salida_TipoSalida");

                entity.Property(e => e.CreadoEl).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descuento).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ModificadoEl).HasMaxLength(10);

                entity.Property(e => e.ModificadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observacion).HasMaxLength(500);

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Salidas)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salida_Area");

                entity.HasOne(d => d.CreadoPorNavigation)
                    .WithMany(p => p.SalidasCreadas)
                    .HasForeignKey(d => d.CreadoPor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salida_PerfilCreado");

                entity.HasOne(d => d.Ente)
                    .WithMany(p => p.Salidas)
                    .HasForeignKey(d => d.EnteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salida_Ente");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Salidas)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salida_Estado");

                entity.HasOne(d => d.FormaPago)
                    .WithMany(p => p.Salidas)
                    .HasForeignKey(d => d.FormaPagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salida_FormaPago");

                entity.HasOne(d => d.ModificadoPorNavigation)
                    .WithMany(p => p.SalidasModificadas)
                    .HasForeignKey(d => d.ModificadoPor)
                    .HasConstraintName("FK_Salida_PerfilModificado");

                entity.HasOne(d => d.SalidaEstado)
                    .WithMany(p => p.Salidas)
                    .HasForeignKey(d => d.SalidaEstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salida_SalidaEstado");

                entity.HasOne(d => d.TipoSalida)
                    .WithMany(p => p.Salidas)
                    .HasForeignKey(d => d.TipoSalidaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salida_TipoSalida");
            });

            modelBuilder.Entity<SalidaDetalle>(entity =>
            {
                entity.HasIndex(e => e.InventarioId)
                    .HasName("IX_SalidaDetalle_Inventario");

                entity.HasIndex(e => e.SalidaId)
                    .HasName("IX_SalidaDetalle_Salida");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Costo).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CostoPromedio).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Descuento).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Precio).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Inventario)
                    .WithMany(p => p.SalidaDetalles)
                    .HasForeignKey(d => d.InventarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalidaDetalle_Inventario");

                entity.HasOne(d => d.Salida)
                    .WithMany(p => p.SalidaDetalles)
                    .HasForeignKey(d => d.SalidaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalidaDetalle_Salida");

                entity.HasOne(d => d.ServicioEstandar)
                    .WithMany(p => p.SalidaDetalles)
                    .HasForeignKey(d => d.ServicioEstandarId)
                    .HasConstraintName("FK_InvSalidaDetalle_ServServicioEstandar");
            });

            modelBuilder.Entity<SalidaEstado>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasIndex(e => e.Descripcion);

                entity.Property(e => e.CreadoEl).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ModificadoEl).HasColumnType("datetime");

                entity.Property(e => e.ModificadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReglaPrecio)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.CreadoPorNavigation)
                    .WithMany(p => p.ServiciosCreados)
                    .HasForeignKey(d => d.CreadoPor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_PerfilCreado");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_Estado");

                entity.HasOne(d => d.ModificadoPorNavigation)
                    .WithMany(p => p.ServiciosModificados)
                    .HasForeignKey(d => d.ModificadoPor)
                    .HasConstraintName("FK_Servicio_PerfilModificado");
            });

            modelBuilder.Entity<ServicioEstandar>(entity =>
            {
                entity.Property(e => e.CreadoEl).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModificadoEl).HasColumnType("datetime");

                entity.Property(e => e.ModificadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreadoPorNavigation)
                    .WithMany(p => p.ServiciosEstandaresCreados)
                    .HasForeignKey(d => d.CreadoPor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicioEstandar_PerfilCreado");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.ServiciosEstandares)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SServicioEstandar_Estado");

                entity.HasOne(d => d.Inventario)
                    .WithMany(p => p.ServiciosEstandares)
                    .HasForeignKey(d => d.InventarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicioEstandar_Inventario");

                entity.HasOne(d => d.ModificadoPorNavigation)
                    .WithMany(p => p.ServiciosEstandaresModificados)
                    .HasForeignKey(d => d.ModificadoPor)
                    .HasConstraintName("FK_ServicioEstandar_PerfilModificado");

                entity.HasOne(d => d.Servicio)
                    .WithMany(p => p.ServiciosEstandares)
                    .HasForeignKey(d => d.ServicioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicioEstandar_Servicio");
            });

            modelBuilder.Entity<Sexo>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoEnte>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoEntrada>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.TipoEntradas)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoEntrada_Estado");
            });

            modelBuilder.Entity<TipoIdentificacion>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoSalida>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.TipoSalidas)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoSalida_Estado");
            });

            modelBuilder.Entity<Um>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Ums)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Um_Estado");
            });
        }
    }
}
