using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IEventService
    {
        Task<Event> AddEvent(EventDto eventDto);
        Task<Event> RemoveEvent(int id);
        Task<Event> UpdateEvent(int id,EventDto eventDto);
        Task<Event?> GetById(int eventId);
        Task<IEnumerable<Event>> GetAllEvents();
        Task<bool> EventExists(int id);
    }
}