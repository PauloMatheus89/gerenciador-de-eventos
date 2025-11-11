using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Exceptions;
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
        //TODO: Adcionar codigo para deletar Inscrição quando Event for deletado

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

            _eventRepository.Remove(@event);
        }

        public void UpdateEvent(int id, Event @event)
        {
            if (@event == null)
                throw new NullReferenceException("Event Cannot be null!");

            var updateEvent = _eventRepository.GetById(id);

            if (IsNewDateAfterStartDate(@event.StartingDate))
                throw new InvalidDateException(@event.StartingDate);

            _eventRepository.Update(id, @event);
        }
         

        //Só é possivel alterar a data se o evento não tiver iniciado
        public bool IsNewDateAfterStartDate(DateTime startDate)
        {
            return startDate.Date >= DateTime.Today.Date;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _eventRepository.GetAllEvents();
        }
    }
}