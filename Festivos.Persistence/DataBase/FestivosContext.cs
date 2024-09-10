using System;
using System.Collections.Generic;
using Festivos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Festivos.API.Models;

public partial class FestivosContext : DbContext
{
    public FestivosContext()
    {
    }

    public FestivosContext(DbContextOptions<FestivosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Festivo> Festivos { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Festivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkFestivo_Id");

            entity.ToTable("Festivo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Festivos)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkFestivo_Tipo");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkTipo_Id");

            entity.ToTable("Tipo");

            entity.Property(e => e.Tipo1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
