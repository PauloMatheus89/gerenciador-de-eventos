using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IEventRepository
    {
        Task<Event> Create(Event @event);
        Task<Event> Update(Event eventToUpdate,Event newEvent);
        Task<Event> Remove(Event @event);
        Task<Event?> GetById(int id);
        Task<IEnumerable<Event>> GetAllEvents();
        IQueryable<Event> Query();
        Task<bool> EventExists(int id);
    }
}