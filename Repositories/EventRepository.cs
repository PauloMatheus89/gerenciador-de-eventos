using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _context;

        public EventRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Create(Event var)
        {
            _context.Events.Add(var);
            _context.SaveChanges();
        }

        public Event GetById(int id)
        {
            var @event = _context.Events
            .Include(e => e.Address)
            .Include(e => e.Category)
            .Include(e => e.Organizer)
            .FirstOrDefault(e => e.Id == id);

            if(@event == null)
            {
                throw new NullReferenceException("Register was not Found!");
            }

            return @event;
        }

        public void Remove(Event @event)
        {
            _context.Events.Remove(@event);
            _context.SaveChanges();
        }

        public void Update(int id, Event @event)
        {
            var updateEvent = _context.Events
            .Include(e => e.Address)
            .Include(e => e.Category)
            .Include(e => e.Organizer)
            .FirstOrDefault(e => e.Id == id);


            if (updateEvent != null)
            {

                if (updateEvent.CategoryId != 0 && updateEvent.CategoryId != @event.CategoryId)
                {
                    var newCategory = _context.Categories.Find(@event.CategoryId);
                    if (newCategory != null)
                    {
                        updateEvent.CategoryId = @event.CategoryId;
                        updateEvent.Category = newCategory;
                    }
                }

                if (updateEvent.OrganizerId != 0 && updateEvent.OrganizerId != @event.OrganizerId)
                {
                    var newOrganizer = _context.Organizers.Find(@event.OrganizerId);
                    if (newOrganizer != null)
                    {
                        updateEvent.OrganizerId = @event.OrganizerId;
                        updateEvent.Organizer = newOrganizer;
                    }
                }

                updateEvent.Title = @event.Title;
                updateEvent.StartingDate = @event.StartingDate; //Verificar se o dia atual ja não é data de inicio
                updateEvent.EndDate = @event.EndDate;
                updateEvent.EntryFee = @event.EntryFee;
                updateEvent.Description = @event.Description;
                updateEvent.AvaiableVacancies = @event.AvaiableVacancies;
                updateEvent.TotalVacancies = @event.AvaiableVacancies;

                _context.SaveChanges();
            }
        }
        
        public IEnumerable<Event> GetAllEvents()
        {
            return _context.Events
                    .Include(e => e.Address)
                    .Include(e => e.Category)
                    .Include(e => e.Organizer)
                    .ToList();
        }
    }
}