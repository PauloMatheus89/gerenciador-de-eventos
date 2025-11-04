using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Domain.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void AddEvent(Event @event)
        {
            if (@event == null)
            {
                throw new NullReferenceException("Event cannot be null!");
            }

            _eventRepository.Create(@event);
        }

        public Event? GetById(int eventId)
        {
            var @event = _eventRepository.GetById(eventId);

            if (@event == null)
            {
                return null;
            }

            return @event;
        }

        public void RemoveEvent(Event @event)
        {
            if (@event == null)
            {
                throw new NullReferenceException("Event cannot be null!");
            }
            
            
        }

        public void UpdateEvent(int id, Event var)
        {
            throw new NotImplementedException();
        }
    }
}