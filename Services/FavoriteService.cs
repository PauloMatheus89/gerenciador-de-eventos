using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;

namespace GerenciadorEventos.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<Favorite> AddFavorite(FavoriteDTO favoriteDTO)
        {
            if (favoriteDTO is null)
                throw new ArgumentNullException("FavoriteDto received is null!");

            var favorite = new Favorite
            {
                EventId = favoriteDTO.EventId,
                UserId = favoriteDTO.UserId
            };

            var newFavorite = await _favoriteRepository.Create(favorite);

            return newFavorite;
        }

        public async Task<Favorite> Delete(int id)
        {
            var favoriteToDelete = await _favoriteRepository.GetById(id);

            if (favoriteToDelete is null)
                throw new KeyNotFoundException("Could not find a Favorite related to that id to delete");

            var favorite = await _favoriteRepository.Remove(favoriteToDelete);

            return favorite;
        }

        public async Task<IEnumerable<Favorite>> GetAllFavorites()
        {
            return await _favoriteRepository.GetAllFavorites();
        }

        public async Task<Favorite?> GetFavorite(int id)
        {
            return await _favoriteRepository.GetById(id);
        }

        public async Task<Favorite> Update(int id, FavoriteDTO favoriteDTO)
        {
            var favoriteToUpdate = await _favoriteRepository.GetById(id);

            if (favoriteToUpdate is null)
                throw new KeyNotFoundException("Could not find a Favorite related to that Id");

            if (favoriteDTO is null)
                throw new ArgumentNullException("favoriteDto received is null!");

            var newFavorite = new Favorite
            {
                EventId = favoriteDTO.EventId,
                UserId = favoriteDTO.UserId
            };

            var updatedFavorite = await _favoriteRepository.Update(favoriteToUpdate, newFavorite);

            return updatedFavorite;
        }
    }
}