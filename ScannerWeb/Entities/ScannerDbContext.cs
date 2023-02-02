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

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("Server=tcp:scanner3eadi-server.database.windows.net,1433;Initial Catalog=ScannerDB;Persist Security Info=False;User ID=scannerAdmin;Password=projektskaner123@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Polish_CI_AS");

        modelBuilder.Entity<Budynek>(entity =>
        {
            entity.HasKey(e => e.IdBudynku).HasName("PK__Budynek__9433557068FB69F9");

            entity.ToTable("Budynek");

            entity.Property(e => e.IdBudynku).HasColumnName("idBudynku");
            entity.Property(e => e.IdSkanera).HasColumnName("idSkanera");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Historium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3213E83F77815BB5");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Czas).HasColumnName("czas");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data_");
            entity.Property(e => e.IdOsoby).HasColumnName("idOsoby");
            entity.Property(e => e.IdSkanera).HasColumnName("idSkanera");
            entity.Property(e => e.TypAutoryzacji)
                .HasMaxLength(10)
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
            entity.HasKey(e => e.Uid).HasName("PK__Karty__C5B196025F10FA8D");

            entity.ToTable("Karty");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.KodOtwarcia).HasColumnName("kodOtwarcia");
        });

        modelBuilder.Entity<Pracownik>(entity =>
        {
            entity.HasKey(e => e.IdPracownik).HasName("PK__Pracowni__6996DB75B7F23A6E");

            entity.ToTable("Pracownik");

            entity.HasIndex(e => e.Pesel, "UQ__Pracowni__48A5F717A709FC17").IsUnique();

            entity.Property(e => e.IdPracownik).HasColumnName("idPracownik");
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
                .HasConstraintName("idKartyFK");
        });

        modelBuilder.Entity<Skaner>(entity =>
        {
            entity.HasKey(e => e.IdSkanera).HasName("PK__Skaner__B8C65DBF1EEDFBC7");

            entity.ToTable("Skaner");

            entity.Property(e => e.IdSkanera).HasColumnName("idSkanera");
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
