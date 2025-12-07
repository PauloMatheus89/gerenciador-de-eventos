using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GerenciadorEventos.Repositories.RepositoryClasses
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DatabaseContext _context;

        public FavoriteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Favorite> Create(Favorite favorite)
        {
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();

            return favorite;
        }

        public async Task<IEnumerable<Favorite>> GetAllFavorites()
        {
            return await _context.Favorites
                .Include(e => e.Event)
                .Include(e => e.User)
                .Include(e => e.EventFavorites)
                .ToListAsync();
        }

        public async Task<Favorite?> GetById(int id)
        {
            return await _context.Favorites
                .Include(e => e.Event)
                .Include(e => e.User)
                .Include(e => e.EventFavorites)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Favorite> Remove(Favorite favorite)
        {
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return favorite;
        }

        public async Task<Favorite> Update(Favorite favoriteToUpdate, Favorite newFavorite)
        {
            favoriteToUpdate.EventId = newFavorite.EventId;
            favoriteToUpdate.UserId = newFavorite.UserId;

            await _context.SaveChangesAsync();

            return favoriteToUpdate;
        }
    }
}