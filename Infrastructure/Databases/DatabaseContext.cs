using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Infrastructure.Databases
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Organizer> Organizers { get; set; } = default!;
        public DbSet<Inscription> Inscriptions { get; set; } = default!;
        public DbSet<Event> Events { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Address> Addresses { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<Day> Days { get; set; } = default!;
        public DbSet<Activity> Activities { get; set; } = default!;
        public DbSet<Participant> Participants { get; set; } = default!;
        public DbSet<Favorite> Favorites { get; set; } = default!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participant>()
                .HasOne(e => e.User)
                .WithOne(e => e.Participant)
                .HasForeignKey<Participant>(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<Organizer>()
                .HasOne(e => e.User)
                .WithOne(e => e.Organizer)
                .HasForeignKey<Organizer>(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Inscription>()
                .HasOne(e => e.User)
                .WithMany(e => e.Inscriptions)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inscription>()
                .HasOne(e => e.Event)
                .WithMany(e => e.Inscriptions)
                .HasForeignKey(e => e.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inscription>()
                .HasOne(e => e.Payment)
                .WithOne(e => e.Inscription)
                .HasForeignKey<Inscription>(e => e.PaymentId)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .HasOne(e => e.Organizer)
                .WithOne(e => e.Address)
                .HasForeignKey<Address>(e => e.OrganizerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Address>()
                .HasOne(e => e.Day)
                .WithOne(e => e.Address)
                .HasForeignKey<Address>(e => e.DayId)
                .IsRequired();

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.OrganizerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Day>()
                .HasOne(e => e.Event)
                .WithMany(e => e.Days)
                .HasForeignKey(e => e.EventId)
                .IsRequired();

            modelBuilder.Entity<Activity>()
                .HasOne(e => e.Day)
                .WithMany(e => e.Activities)
                .HasForeignKey(e => e.DayId)
                .IsRequired();

            modelBuilder.Entity<Favorite>()
                .HasOne(e => e.User)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<Favorite>()
                .HasOne(e => e.Event)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "PauloM",
                    Password = "pk000000",
                    Role = Enums.Role.Client,
                    Email = "abc@gmail.com"
                }
            );
        }
    }
}