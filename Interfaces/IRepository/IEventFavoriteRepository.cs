using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IEventFavoriteRepository
    {
        Task<EventFavorite> Create(EventFavorite entity);
        Task<EventFavorite> Delete(EventFavorite entity);
        Task<EventFavorite> Update(EventFavorite entityToUpdate, EventFavorite newEntity);
        Task<EventFavorite?> GetById(int id);
        Task<IEnumerable<EventFavorite>> GetAllEventFavorite();
    }
}