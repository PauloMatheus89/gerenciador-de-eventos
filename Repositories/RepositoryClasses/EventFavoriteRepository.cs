using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories.RepositoryClasses
{
    public class EventFavoriteRepository : IEventFavoriteRepository
    {

        private readonly DatabaseContext _context;
        public EventFavoriteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<EventFavorite> Create(EventFavorite entity)
        {
            await _context.EventFavorites.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<EventFavorite> Delete(EventFavorite entity)
        {
            _context.EventFavorites.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<EventFavorite>> GetAllEventFavorite()
        {
            return await _context.EventFavorites
                .Include(e => e.Event)
                .Include(e => e.Favorite)
                .ToListAsync();
        }

        public async Task<EventFavorite?> GetById(int id)
        {
            return await _context.EventFavorites
                .Include(e => e.Event)
                .Include(e => e.Favorite)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EventFavorite> Update(EventFavorite entityToUpdate, EventFavorite newEntity)
        {
            entityToUpdate.EventId = newEntity.EventId;
            entityToUpdate.FavoriteId = newEntity.FavoriteId;

            await _context.SaveChangesAsync();

            return entityToUpdate;
        }
    }
}