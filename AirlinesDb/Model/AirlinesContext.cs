using System;
using AirlinesDb.Connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AirlinesDb.Model
{
    public partial class AirlinesContext : DbContext
    {
        public AirlinesContext()
        {
        }

        public AirlinesContext(DbContextOptions<AirlinesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<PassInTrip> PassInTrips { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(new ConnectionStringConfig().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.IdComp)
                    .HasName("PK__Company__744AB6C984E6900E");

                entity.ToTable("Company");

                entity.Property(e => e.IdComp)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_comp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PassInTrip>(entity =>
            {
                entity.HasKey(e => new { e.TripNo, e.Date, e.IdPsg })
                    .HasName("PK__Pass_in___ECA0743998ECD846");

                entity.ToTable("Pass_in_trip");

                entity.Property(e => e.TripNo).HasColumnName("trip_no");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.IdPsg).HasColumnName("ID_psg");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("place")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdPsgNavigation)
                    .WithMany(p => p.PassInTrips)
                    .HasForeignKey(d => d.IdPsg)
                    .HasConstraintName("FK__Pass_in_t__ID_ps__412EB0B6");

                entity.HasOne(d => d.TripNoNavigation)
                    .WithMany(p => p.PassInTrips)
                    .HasForeignKey(d => d.TripNo)
                    .HasConstraintName("FK__Pass_in_t__trip___403A8C7D");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => e.IdPsg)
                    .HasName("PK__Passenge__18AE3700DF64D343");

                entity.ToTable("Passenger");

                entity.Property(e => e.IdPsg)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_psg");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.TripNo)
                    .HasName("PK__Trip__302538112BE2F622");

                entity.ToTable("Trip");

                entity.Property(e => e.TripNo)
                    .ValueGeneratedNever()
                    .HasColumnName("trip_no");

                entity.Property(e => e.IdComp).HasColumnName("ID_comp");

                entity.Property(e => e.Plane)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("plane")
                    .IsFixedLength(true);

                entity.Property(e => e.TimeIn).HasColumnName("time_in");

                entity.Property(e => e.TimeOut).HasColumnName("time_out");

                entity.Property(e => e.TownFrom)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("town_from")
                    .IsFixedLength(true);

                entity.Property(e => e.TownTo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("town_to")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdCompNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.IdComp)
                    .HasConstraintName("FK__Trip__ID_comp__4222D4EF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
