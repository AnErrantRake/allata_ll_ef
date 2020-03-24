using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace EFAnnotations.Models
{
    public partial class PersonHistoryContext : DbContext
    {
        public PersonHistoryContext()
        {
        }

        public PersonHistoryContext(DbContextOptions<PersonHistoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Birth> Birth { get; set; }
        public virtual DbSet<Death> Death { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonAttendedEvent> PersonAttendedEvent { get; set; }
        public virtual DbSet<PersonLivedAtLocation> PersonLivedAtLocation { get; set; }
        
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(MyLoggerFactory)
                    .UseSqlServer("Data Source=.;Initial Catalog=PersonHistory;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Birth>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BirthDesc).IsUnicode(false);
            });

            modelBuilder.Entity<Death>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DeathDesc).IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EventDesc).IsUnicode(false);

                entity.Property(e => e.EventName).IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_Location");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.StateAbbr).IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.NameFirst).IsUnicode(false);

                entity.Property(e => e.NameLast).IsUnicode(false);

                entity.Property(e => e.NameMiddle).IsUnicode(false);

                entity.HasOne(d => d.Birth)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.BirthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_Birth");

                entity.HasOne(d => d.Death)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.DeathId)
                    .HasConstraintName("FK_Person_Death");

                entity.HasOne(d => d.Father)
                    .WithMany(p => p.InverseFather)
                    .HasForeignKey(d => d.FatherId)
                    .HasConstraintName("FK_Person_Father");

                entity.HasOne(d => d.Mother)
                    .WithMany(p => p.InverseMother)
                    .HasForeignKey(d => d.MotherId)
                    .HasConstraintName("FK_Person_Mother");
            });

            modelBuilder.Entity<PersonAttendedEvent>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.EventId });

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.PersonAttendedEvent)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attended_Event");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAttendedEvent)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_Attended");
            });

            modelBuilder.Entity<PersonLivedAtLocation>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.LocationId });

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PersonLivedAtLocation)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LivedAt_Location");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonLivedAtLocation)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_LivedAt");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
