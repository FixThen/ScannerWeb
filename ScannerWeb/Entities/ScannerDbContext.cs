using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScannerWeb.Entities;

public partial class ScannerDbContext : DbContext
{
    public ScannerDbContext()
    {
    }

    public ScannerDbContext(DbContextOptions<ScannerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Budynek> Budyneks { get; set; }

    public virtual DbSet<Historium> Historia { get; set; }

    public virtual DbSet<Karty> Karties { get; set; }

    public virtual DbSet<Pracownik> Pracowniks { get; set; }

    public virtual DbSet<Skaner> Skaners { get; set; }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=scanner3eadi-server.database.windows.net,1433;Initial Catalog=ScannerDB;Persist Security Info=False;User ID=scannerAdmin;Password=projektskaner123@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Polish_CI_AS");

        modelBuilder.Entity<Budynek>(entity =>
        {
            entity.HasKey(e => e.IdBudynku).HasName("PK__Budynek__943355701B80696F");

            entity.ToTable("Budynek");

            entity.Property(e => e.IdBudynku)
                .ValueGeneratedNever()
                .HasColumnName("idBudynku");
            entity.Property(e => e.IdSkanera).HasColumnName("idSkanera");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Historium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3213E83F92E294DF");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Czas).HasColumnName("czas");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data_");
            entity.Property(e => e.IdOsoby).HasColumnName("idOsoby");
            entity.Property(e => e.IdSkanera).HasColumnName("idSkanera");
            entity.Property(e => e.TypAutoryzacji)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("typAutoryzacji");

            entity.HasOne(d => d.IdOsobyNavigation).WithMany(p => p.Historia)
                .HasForeignKey(d => d.IdOsoby)
                .HasConstraintName("idPracFK");

            entity.HasOne(d => d.IdSkaneraNavigation).WithMany(p => p.Historia)
                .HasForeignKey(d => d.IdSkanera)
                .HasConstraintName("idSkanFK");
        });

        modelBuilder.Entity<Karty>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__Karty__C5B19602E1557BBA");

            entity.ToTable("Karty");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.IdOsoby).HasColumnName("id_osoby");
            entity.Property(e => e.KodOtwarcia).HasColumnName("kodOtwarcia");
        });

        modelBuilder.Entity<Pracownik>(entity =>
        {
            entity.HasKey(e => e.IdPracownik).HasName("PK__Pracowni__6996DB75501E6B54");

            entity.ToTable("Pracownik");

            entity.HasIndex(e => e.Pesel, "UQ__Pracowni__48A5F7173306146B").IsUnique();

            entity.Property(e => e.IdPracownik)
                .ValueGeneratedNever()
                .HasColumnName("idPracownik");
            entity.Property(e => e.IdKarty).HasColumnName("idKarty");
            entity.Property(e => e.Imie)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NumerTelefonu)
                .HasColumnType("text")
                .HasColumnName("numerTelefonu");
            entity.Property(e => e.Pesel)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PoziomDostepu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("poziomDostepu");

            entity.HasOne(d => d.IdKartyNavigation).WithMany(p => p.Pracowniks)
                .HasForeignKey(d => d.IdKarty)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("idKartyFK");
        });

        modelBuilder.Entity<Skaner>(entity =>
        {
            entity.HasKey(e => e.IdSkanera).HasName("PK__Skaner__B8C65DBFFE39F343");

            entity.ToTable("Skaner");

            entity.Property(e => e.IdSkanera)
                .ValueGeneratedNever()
                .HasColumnName("idSkanera");
            entity.Property(e => e.IdBudynku).HasColumnName("idBudynku");
            entity.Property(e => e.KodOtwarcia).HasColumnName("kodOtwarcia");
            entity.Property(e => e.PoziomDostepu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("poziomDostepu");
            entity.Property(e => e.TypAutoryzacji)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("typAutoryzacji");

            entity.HasOne(d => d.IdBudynkuNavigation).WithMany(p => p.Skaners)
                .HasForeignKey(d => d.IdBudynku)
                .HasConstraintName("idBudFK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
