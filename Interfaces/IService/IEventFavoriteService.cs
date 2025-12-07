using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IEventFavoriteService
    {
        Task<EventFavorite> AddEventFavorite(EventFavoriteDTO entity);
        Task<EventFavorite> DeleteEventFavorite(int id);
        Task<EventFavorite?> GetEventFavorite(int id);
        Task<EventFavorite> UpdateEventFavorite(int id, EventFavoriteDTO newEntity);
        Task<IEnumerable<EventFavorite>> GetEventFavorites();

    }
}