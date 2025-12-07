using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Exceptions;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;
using Microsoft.IdentityModel.Tokens;

namespace GerenciadorEventos.Domain.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IOrganizerRepository _organizerRepository;
        public EventService(IEventRepository eventRepository, IOrganizerRepository organizerRepository)
        {
            _eventRepository = eventRepository;
            _organizerRepository = organizerRepository;
        }

        public async Task<Event> AddEvent(EventDto eventDto)
        {
            if (eventDto == null)
            {
                throw new NullReferenceException("Event cannot be null!");
            }

            if (EventExist(eventDto))
                throw new DuplicateNameException("Error in AddEvent(Event) - This Event already exists in the database!");

            var newEvent = new Event
            {
                StartingDate = eventDto.StartingDate,
                EndDate = eventDto.EndDate,
                EntryFee = eventDto.EntryFee,
                TotalVacancies = eventDto.TotalVacancies,
                AvaiableVacancies = eventDto.AvaiableVacancies,
                Title = eventDto.Title,
                Description = eventDto.Description,
                OrganizerId = eventDto.OrganizerId,
                CategoryId = eventDto.CategoryId
            };

            var @event = await _eventRepository.Create(newEvent);
            return @event;
        }

        public async Task<Event?> GetById(int eventId)
        {
            return await _eventRepository.GetById(eventId);
        }

        //TODO: Adcionar codigo para deletar Inscrição quando Event for deletado
        //TODO: Adcionar uma verificação para status do evento, não podendo ser deletado caso esteja em andamento
        public async Task<Event> RemoveEvent(int id)
        {
            var eventToDelete = await _eventRepository.GetById(id);

            if (eventToDelete is null)
                throw new KeyNotFoundException($"Could not find an Event related to the id {id}");

            var @event = await _eventRepository.Remove(eventToDelete);

            return @event;
        }

        public async Task<Event> UpdateEvent(int id, EventDto eventDto)
        {
            var updateEvent = await _eventRepository.GetById(id);

            if (eventDto == null)
                throw new NullReferenceException("Error in UpdateEvent(Event) - Event Cannot be null!");

            if (updateEvent is null)
                throw new KeyNotFoundException($"Could not find an Event related to the id {id}");

            if (EventExist(eventDto))
                throw new DuplicateNameException("Erro in UpdateEvent(Event) - This Event already exists!");

            if (!IsNewStartDateValid(eventDto.StartingDate))
                throw new InvalidDateException(eventDto.StartingDate);

            var newEvent = new Event
            {
                StartingDate = eventDto.StartingDate,
                EndDate = eventDto.EndDate,
                EntryFee = eventDto.EntryFee,
                TotalVacancies = eventDto.TotalVacancies,
                AvaiableVacancies = eventDto.AvaiableVacancies,
                Title = eventDto.Title,
                Description = eventDto.Description
            };

            var @event = await _eventRepository.Update(updateEvent,newEvent);

            return updateEvent;
            
        }
         

        //Só é possivel alterar a data se o evento não tiver iniciado
        public bool IsNewStartDateValid(DateTime startDate)
        {
            return startDate.Date >= DateTime.Today.Date;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _eventRepository.GetAllEvents();
        }

        public bool EventExist(EventDto dto)
        {
            return _eventRepository.Query().Any(e =>
                e.Title == dto.Title &&
                e.OrganizerId == dto.OrganizerId &&
                e.CategoryId == dto.CategoryId &&
                e.StartingDate == dto.StartingDate &&
                e.EndDate == dto.EndDate
            );
        }

        public async Task<bool> EventExists(int id)
        {
            return await _eventRepository.EventExists(id);
        }

        
    }
}