using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirst;

//Scaffold-DbContext 'Server=localhost;Database=TransactionDB;Trusted_Connection=True;TrustServerCertificate=True;' Microsoft.EntityFrameworkCore.SqlServer
public partial class TransactionDbContext : DbContext
{
    public TransactionDbContext()
    {
    }

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Existencia> Existencias { get; set; }

    public virtual DbSet<Folio> Folios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<VentasDetalle> VentasDetalles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=TransactionDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Existencia>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.Producto).WithMany()
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Existencias_Productos");
        });

        modelBuilder.Entity<Folio>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasIndex(e => e.Folio, "IX_Ventas_Folio").IsUnique();

            entity.Property(e => e.Cliente)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VentasDetalle>(entity =>
        {
            entity.ToTable("VentasDetalle");

            entity.HasIndex(e => new { e.VentaId, e.Renglon }, "IX_VentasDetalle_VentaId_Renglon").IsUnique();

            entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Importe).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
