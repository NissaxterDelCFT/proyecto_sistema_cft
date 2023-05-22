using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace proyecto_sistema_cft.Models;

public partial class DbSistemaCftContext : DbContext
{
    public DbSistemaCftContext()
    {
    }

    public DbSistemaCftContext(DbContextOptions<DbSistemaCftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<AsignaturasAsignada> AsignaturasAsignadas { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {

        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignaturas");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(45)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<AsignaturasAsignada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignaturas_asignadas");

            entity.HasIndex(e => e.AsignaturasId, "fk_estudiantes_has_asignaturas_asignaturas1_idx");

            entity.HasIndex(e => e.EstudiantesId, "fk_estudiantes_has_asignaturas_estudiantes_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AsignaturasId)
                .HasColumnType("int(11)")
                .HasColumnName("asignaturas_id");
            entity.Property(e => e.EstudiantesId)
                .HasColumnType("int(11)")
                .HasColumnName("estudiantes_id");
            entity.Property(e => e.FechaRegistro).HasColumnName("fecha_registro");

            entity.HasOne(d => d.Asignaturas).WithMany(p => p.AsignaturasAsignada)
                .HasForeignKey(d => d.AsignaturasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estudiantes_has_asignaturas_asignaturas1");

            entity.HasOne(d => d.Estudiantes).WithMany(p => p.AsignaturasAsignada)
                .HasForeignKey(d => d.EstudiantesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estudiantes_has_asignaturas_estudiantes");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiantes");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(45)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(45)
                .HasColumnName("direccion");
            entity.Property(e => e.Edad)
                .HasColumnType("int(11)")
                .HasColumnName("edad");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Rut)
                .HasMaxLength(45)
                .HasColumnName("rut");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
