using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using GerenciadorEventos.Repositories.HelpfulMethods;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly DatabaseContext _context;

        public OrganizerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Organizer> Create(Organizer organizer)
        {
            await _context.Organizers.AddAsync(organizer);
            _context.SaveChanges();

            return organizer;
        }

        public async Task<Organizer> Delete(Organizer organizer)
        {
            _context.Organizers.Remove(organizer);

            await _context.SaveChangesAsync();

            return organizer;

        }

        public async Task<Organizer?> GetByid(int? id)
        {
            return await _context.Organizers
                            .Include(o => o.User)
                            .Include(o => o.Address)
                            .FirstOrDefaultAsync(o => o.Id == id);

            
        }

        public async Task<Organizer?> GetByDocument(string document)
        {
            return await _context.Organizers
                            .Include(o => o.User)
                            .Include(o => o.Address)
                            .FirstOrDefaultAsync(o => o.Document == document);
        }

        public async Task<Organizer?> GetByCorporateEmail(string corporateEmail)
        {
            return await _context.Organizers
                            .Include(o => o.User)
                            .Include(o => o.Address)
                            .FirstOrDefaultAsync(o => o.CorporateEmail == corporateEmail);
        }

        
        public async Task<Organizer> Update(Organizer organizerToUpdate, Organizer newOrganizer)
        {
            organizerToUpdate.CorporateEmail = newOrganizer.CorporateEmail;
            organizerToUpdate.CorporateName = newOrganizer.CorporateName;
            organizerToUpdate.Description = newOrganizer.Description;
            organizerToUpdate.Document = newOrganizer.Document;
            
            await _context.SaveChangesAsync();

            return organizerToUpdate;

        }

        public async Task<IEnumerable<Organizer>> GetAllOrganizers()
        {
            return await _context.Organizers
            .Include(o => o.Address)
            .Include(o => o.User)
            .ToListAsync();
        }

        public async Task<bool> DocumentExists(string document)
        {
            return await _context.Organizers.AnyAsync(e => e.Document == document);
        }

        public async Task<bool> CorporateEmailExists(string corporateEmail)
        {
            return await _context.Organizers.AnyAsync(e => e.CorporateEmail == corporateEmail);
        }

    }
}