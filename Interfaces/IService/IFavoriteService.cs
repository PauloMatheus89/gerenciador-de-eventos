using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IFavoriteService
    {
        Task<Favorite> AddFavorite(FavoriteDTO favoriteDTO);
        Task<Favorite> Delete(int id);
        Task<Favorite> Update(int id, FavoriteDTO favoriteDTO);
        Task<Favorite?> GetFavorite(int id);
        Task<IEnumerable<Favorite>> GetAllFavorites();

    }
}