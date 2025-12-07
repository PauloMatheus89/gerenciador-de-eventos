using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Domain.Models.Enums;
using GerenciadorEventos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GerenciadorEventos.Infrastructure.Databases
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Organizer> Organizers { get; set; } = default!;
        public DbSet<Inscription> Inscriptions { get; set; } = default!;
        public DbSet<EventFavorite> EventFavorites { get; set; } = default!;
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
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Organizer>()
                .HasOne(e => e.User)
                .WithOne(e => e.Organizer)
                .HasForeignKey<Organizer>(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inscription>()
                .HasOne(e => e.User)
                .WithMany(e => e.Inscriptions)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscription>()
                .HasOne(e => e.Event)
                .WithMany(e => e.Inscriptions)
                .HasForeignKey(e => e.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscription>()
                .HasOne(e => e.Payment)
                .WithOne(e => e.Inscription)
                .HasForeignKey<Inscription>(e => e.PaymentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(e => e.Organizer)
                .WithOne(e => e.Address)
                .HasForeignKey<Address>(e => e.OrganizerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(e => e.Day)
                .WithOne(e => e.Address)
                .HasForeignKey<Address>(e => e.DayId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.OrganizerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventFavorites)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Day>()
                .HasOne(e => e.Event)
                .WithMany(e => e.Days)
                .HasForeignKey(e => e.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Activity>()
                .HasOne(e => e.Day)
                .WithMany(e => e.Activities)
                .HasForeignKey(e => e.DayId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(e => e.User)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasMany(e => e.EventFavorites)
                .WithOne(e => e.Favorite)
                .HasForeignKey(e => e.FavoriteId)
                .OnDelete(DeleteBehavior.NoAction);

            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = "1",
                UserName = "PauloM",
                NormalizedUserName = "PAULOM",
                Email = "abc@gmail.com",
                NormalizedEmail = "ABC@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            user.PasswordHash = hasher.HashPassword(user, "pk000000");

            modelBuilder.Entity<User>().HasData(user);

        }
    }
}