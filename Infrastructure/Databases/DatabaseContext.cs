using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(o => o.Event)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict); // ou NoAction

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Address)
                .WithOne(a => a.Event)
                .HasForeignKey<Event>(e => e.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.Address)
                .WithOne(a => a.Organizer)
                .HasForeignKey<Organizer>(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.User)
                .WithOne(u => u.Organizer)
                .HasForeignKey<Organizer>(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(o => o.Event)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict); // ou NoAction

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Address)
                .WithOne(a => a.Event)
                .HasForeignKey<Event>(e => e.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.Address)
                .WithOne(a => a.Organizer)
                .HasForeignKey<Organizer>(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.User)
                .WithOne(u => u.Organizer)
                .HasForeignKey<Organizer>(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

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