using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Contexts;

public partial class TQContext : DbContext
{
    public TQContext()
    {
    }

    public TQContext(DbContextOptions<TQContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("estados_pkey");

            entity.ToTable("estados");

            entity.HasIndex(e => e.Nombre, "estados_nombre_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "roles_nombre_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tareas_pkey");

            entity.ToTable("tareas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estadoid).HasColumnName("estadoid");
            entity.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaactualizacion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasColumnName("titulo");
            entity.Property(e => e.Usuarioasignadoid).HasColumnName("usuarioasignadoid");

            entity.HasOne(d => d.Estado).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.Estadoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tareas_estadoid_fkey");

            entity.HasOne(d => d.Usuarioasignado).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.Usuarioasignadoid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tareas_usuarioasignadoid_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Correo, "usuarios_correo_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena).HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Rolid).HasColumnName("rolid");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Rolid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_rolid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
