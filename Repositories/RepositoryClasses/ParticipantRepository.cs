using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GerenciadorEventos.Repositories.RepositoryClasses
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly DatabaseContext _context;

        public ParticipantRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Participant> Create(Participant participant)
        {
            await _context.Participants.AddAsync(participant);

            await _context.SaveChangesAsync();

            return participant;
        }

        public async Task<Participant> Delete(Participant participant)
        {
            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();

            return participant;
        }

        public async Task<IEnumerable<Participant>> GetAllParticipants()
        {
            return await _context.Participants.Include(p => p.User).ToListAsync();
        }

        public async Task<Participant?> GetById(int id)
        {
            return await _context.Participants
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Participant> Update(Participant participantToUpdate, Participant newParticipant)
        {
            participantToUpdate.Email = newParticipant.Email;
            participantToUpdate.Name = newParticipant.Name;

            await _context.SaveChangesAsync();

            return participantToUpdate;
        }

        public async Task<bool> ParticipantExists(int id)
        {
            return await _context.Participants.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> ParticipantExists(string email)
        {
            return await _context.Participants.AnyAsync(e => e.Email == email);
        }
    }
}