using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<ContentAdmin> ContentAdmins { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Screening> Screenings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-OLO8K19\\SQLEXPRESS;Database=mydb;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__CINEMAS__737584F7AE37B029");
        });

        modelBuilder.Entity<ContentAdmin>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK_Table_1");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__MOVIES__D9C1FA01CA8F61DD");

            entity.HasOne(d => d.ContentAdminsUsernameNavigation).WithMany(p => p.Movies).HasConstraintName("FK__MOVIES__CONTENT___ADMINS");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RESERVAT__3214EC2707F82F7D");

            entity.HasOne(d => d.CustomersUsernameNavigation).WithMany(p => p.Reservations).HasConstraintName("FK__RESERVATI__CUSTO__43D61337");

            entity.HasOne(d => d.ScreeningsCinemasNameNavigation).WithMany(p => p.Reservations).HasConstraintName("FK__RESERVATI__SCREE__42E1EEFE");

            entity.HasOne(d => d.ScreeningsMoviesNameNavigation).WithMany(p => p.Reservations).HasConstraintName("FK__RESERVATI__SCREE__41EDCAC5");
        });

        modelBuilder.Entity<Screening>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SCREENIN__3214EC2737B59509");

            entity.HasOne(d => d.CinemasNameNavigation).WithMany(p => p.Screenings).HasConstraintName("FK_CINEMAS_NAME");

            entity.HasOne(d => d.ContentAdminsUsernameNavigation).WithMany(p => p.Screenings).HasConstraintName("FK_CONTENT_ADMINS_USERNAME");

            entity.HasOne(d => d.MoviesNameNavigation).WithMany(p => p.Screenings).HasConstraintName("FK_MOVIES_NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
