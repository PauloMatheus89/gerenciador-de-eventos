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
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _context;

        public EventRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Event> Create(Event @event)
        {
            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        public async Task<Event?> GetById(int id)
        {
            return await _context.Events
            .Include(e => e.Days)
            .Include(e => e.Category)
            .Include(e => e.Organizer)
            .FirstOrDefaultAsync(e => e.Id == id);

        }

        public async Task<Event> Remove(Event @event)
        {
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        public async Task<Event> Update(Event eventToUpdate, Event newEvent)
        {
            eventToUpdate.StartingDate = newEvent.StartingDate;
            eventToUpdate.TotalVacancies = newEvent.TotalVacancies;
            eventToUpdate.AvaiableVacancies = newEvent.AvaiableVacancies;
            eventToUpdate.Description = newEvent.Description;
            eventToUpdate.EndDate = newEvent.EndDate;
            eventToUpdate.EntryFee = newEvent.EntryFee;
            eventToUpdate.Title = newEvent.Title;

            await _context.SaveChangesAsync();

            return eventToUpdate;
            
        }
        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _context.Events
                    .Include(e => e.Days)
                    .Include(e => e.Category)
                    .Include(e => e.Organizer)
                    .ToListAsync();
        }

        public IQueryable<Event> Query()
        {
            return _context.Events.AsQueryable();
        }

        public async Task<bool> EventExists(int id)
        {
            return await _context.Events.AnyAsync(e => e.Id == id);
        }

    }
}