using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using GerenciadorEventos.Repositories.HelpfulMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace GerenciadorEventos.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByid(string id)
        {
            return  await _context.Users
                        .Include(u => u.Participant)
                        .Include(u => u.Organizer)
                        .Include(u => u.Payments)
                        .Include(u => u.Favorites)
                        .Include(u => u.Inscriptions)
                        .FirstOrDefaultAsync(u => u.Id == id);

        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                        .Include(u => u.Participant)
                        .Include(u => u.Organizer)
                        .Include(u => u.Payments)
                        .Include(u => u.Favorites)
                        .Include(u => u.Inscriptions)
                        .FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                    .Include(e => e.Participant)
                    .Include(e => e.Favorites)
                    .Include(e => e.Organizer)
                    .Include(e => e.Payments)
                    .Include(e => e.Payments).ToListAsync();
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(e => e.Email == email);
        }

        public async Task<bool> UserExists(string id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }

        
    }
}