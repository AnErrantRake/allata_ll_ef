using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace EFFluidAPI.Models
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
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.BirthDesc)
                    .HasColumnName("birth_desc")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Death>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeathDate)
                    .HasColumnName("death_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeathDesc)
                    .HasColumnName("death_desc")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EventDate)
                    .HasColumnName("event_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventDesc)
                    .IsRequired()
                    .HasColumnName("event_desc")
                    .IsUnicode(false);

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("event_name")
                    .IsUnicode(false);

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_Location");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address1)
                    .HasColumnName("address1")
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .IsUnicode(false);

                entity.Property(e => e.CoordLat).HasColumnName("coord_lat");

                entity.Property(e => e.CoordLon).HasColumnName("coord_lon");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .IsUnicode(false);

                entity.Property(e => e.StateAbbr)
                    .HasColumnName("state_abbr")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthId).HasColumnName("birth_id");

                entity.Property(e => e.DeathId).HasColumnName("death_id");

                entity.Property(e => e.FatherId).HasColumnName("father_id");

                entity.Property(e => e.MotherId).HasColumnName("mother_id");

                entity.Property(e => e.NameFirst)
                    .IsRequired()
                    .HasColumnName("name_first")
                    .IsUnicode(false);

                entity.Property(e => e.NameLast)
                    .IsRequired()
                    .HasColumnName("name_last")
                    .IsUnicode(false);

                entity.Property(e => e.NameMiddle)
                    .HasColumnName("name_middle")
                    .IsUnicode(false);

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

                entity.ToTable("PersonAttended_Event");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

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

                entity.ToTable("PersonLived_AtLocation");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("datetime");

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
