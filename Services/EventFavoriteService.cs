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
    public class EventFavoriteService : IEventFavoriteService
    {
        private readonly IEventFavoriteRepository _eventFavoriteRepository;

        public EventFavoriteService(IEventFavoriteRepository eventFavoriteRepository)
        {
            _eventFavoriteRepository = eventFavoriteRepository;
        }

        public async Task<EventFavorite> AddEventFavorite(EventFavoriteDTO entity)
        {
            if (entity is null)
                throw new ArgumentNullException("DTO received is null!");

            var eventFavorite = new EventFavorite
            {
                EventId = entity.EventId,
                FavoriteId = entity.FavoriteId
            };

            var newEventFavorite = await _eventFavoriteRepository.Create(eventFavorite);

            return newEventFavorite;
        }

        public async Task<EventFavorite> DeleteEventFavorite(int id)
        {
            var eventFavorite = await _eventFavoriteRepository.GetById(id);

            if (eventFavorite is null)
                throw new KeyNotFoundException("Could not find an EventFavorite related that Id ");

            var eventFavoriteToDelete = await _eventFavoriteRepository.Delete(eventFavorite);

            return eventFavoriteToDelete;
        }

        public async Task<EventFavorite?> GetEventFavorite(int id)
        {
            return await _eventFavoriteRepository.GetById(id);
        }

        public async Task<IEnumerable<EventFavorite>> GetEventFavorites()
        {
            return await _eventFavoriteRepository.GetAllEventFavorite();
        }

        public async Task<EventFavorite> UpdateEventFavorite(int id, EventFavoriteDTO newEntity)
        {
            var eventFavoriteToUpdate = await _eventFavoriteRepository.GetById(id);

            if (eventFavoriteToUpdate is null)
                throw new KeyNotFoundException("Could not find an EventFavorite related that Id");

            if (newEntity is null)
                throw new ArgumentNullException("DTO received is null");

            var newEventFavorite = new EventFavorite
            {
                EventId = newEntity.EventId,
                FavoriteId = newEntity.FavoriteId
            };

            var eventFavorite = await _eventFavoriteRepository.Update(eventFavoriteToUpdate, newEventFavorite);

            return eventFavorite;
        }

        
    }
}