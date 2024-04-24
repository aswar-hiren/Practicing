using System;
using System.Collections.Generic;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DataContext;

public partial class HellodocPrjContext : DbContext
{
    public HellodocPrjContext()
    {
    }

    public HellodocPrjContext(DbContextOptions<HellodocPrjContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Examdb; Password=admin#Aswar2002; Username=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("City_pkey");

            entity.ToTable("City");

            entity.Property(e => e.CityId)
                .ValueGeneratedNever()
                .HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasColumnType("character varying")
                .HasColumnName("cityName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Userid)
                .HasIdentityOptions(2L, null, null, null, null, null)
                .HasColumnName("userid");
            entity.Property(e => e.Birthdate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.City).HasColumnType("character varying");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Country).HasColumnType("character varying");
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("firstName");
            entity.Property(e => e.Gender).HasColumnType("character varying");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("lastName");
            entity.Property(e => e.PhoneNumber).HasColumnType("character varying");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("city");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
