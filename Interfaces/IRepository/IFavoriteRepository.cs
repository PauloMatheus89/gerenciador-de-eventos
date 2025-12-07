using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IFavoriteRepository
    {
        Task<Favorite> Create(Favorite Favorite);
        Task<Favorite> Remove(Favorite favorite);
        Task<Favorite> Update(Favorite favoriteToUpdate, Favorite newFavorite);
        Task<Favorite?> GetById(int id);
        Task<IEnumerable<Favorite>> GetAllFavorites();
    }
}