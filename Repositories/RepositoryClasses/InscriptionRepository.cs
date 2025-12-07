using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories.RepositoryClasses
{
    public class InscriptionRepository : IInscriptionRepository
    {
        private readonly DatabaseContext _context;

        public InscriptionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Inscription> Create(Inscription inscription)
        {
            await _context.Inscriptions.AddAsync(inscription);
            await _context.SaveChangesAsync();

            return inscription;
        }

        public async Task<Inscription?> GetById(int id)
        {
            return await _context.Inscriptions.Include(e => e.Event).Include(e => e.User).Include(e => e.Payment).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Inscription> Remove(Inscription inscriptionToDelete)
        {
            _context.Inscriptions.Remove(inscriptionToDelete);
            await _context.SaveChangesAsync();

            return inscriptionToDelete;
        }

        public async Task<Inscription> Update(Inscription inscriptionToUpdate, Inscription inscription)
        {
            inscriptionToUpdate.Status = inscription.Status;
            inscriptionToUpdate.InscriptionDate = inscription.InscriptionDate;

            await _context.SaveChangesAsync();

            return inscriptionToUpdate;

        }

        public async Task<IEnumerable<Inscription>> GetAllInscriptions()
        {
            return await _context.Inscriptions
                .Include(e => e.EventId)
                .Include(e => e.PaymentId)
                .Include(e => e.UserId)
                .ToListAsync();
        }
    }
}