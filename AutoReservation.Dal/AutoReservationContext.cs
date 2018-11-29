using System.Configuration;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AutoReservation.Dal
{
    public class AutoReservationContext
        : DbContext
        
    {
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Reservation> Reservationen { get; set; }


        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(
            new[] { new ConsoleLoggerProvider((_, logLevel) => logLevel >= LogLevel.Information, true) }
        );

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(LoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                    .UseSqlServer(ConfigurationManager.ConnectionStrings[nameof(AutoReservationContext)].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kunde>()
                .ToTable("Kunde")
                .Property(p => p.RowVersion);

            modelBuilder.Entity<Auto>()
                .ToTable("Auto")
                .Property(p => p.RowVersion);

            modelBuilder.Entity<Reservation>()
                .ToTable("Reservation")
                .HasOne(p => p.Kunde)
                .WithMany(b => b.Reservationen)
                .HasForeignKey(p => p.KundeId)
                .HasConstraintName("FK_Reservation_KundeId");

            modelBuilder.Entity<Reservation>()
                .ToTable("Reservation")
                .HasOne(p => p.Auto)
                .WithMany(b => b.Reservationen)
                .HasForeignKey(p => p.AutoId)
                .HasConstraintName("FK_Reservation_AutoId");

            // TODO does this work?
        }
    }
}
