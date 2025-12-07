using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IEntities;
using GerenciadorEventos.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class EventMethods
    {
        public static bool EventExists(DatabaseContext context, Event? @event)
        {
            if (@event != null && context.Events.Any(a => a.Id == @event.Id))
                return true;

            return false;
        }

        public static bool EventExists(DatabaseContext context, EventDto? eventDto)
        {
            if (eventDto == null)
                throw new ArgumentNullException("Error in EventsExists - EventDto received is null");

            var @event = context.Events.FirstOrDefault(e =>
                e.CategoryId == eventDto.CategoryId &&
                e.Title == eventDto.Title &&
                e.OrganizerId == eventDto.OrganizerId &&
                e.StartingDate == eventDto.StartingDate &&
                e.EndDate == eventDto.EndDate
            );

            if (@event != null)
                return true;

            return false;
        }

        public static ICollection<Event> FindEventsToDelete(ICollection<Event> presentList, ICollection<Event> newList)
        {
            return presentList.Where(e => !newList.Any(evt => e.Id == evt.Id)).ToList();
        }

        public static bool EventIdIsDifferent<T>(T entityToUpdate, T entity) where T : IEntityWithEvent
        {
            return entityToUpdate.EventId != 0 && entityToUpdate.EventId != entity.EventId;
        }

        public static void UpdateEventId<T>(DatabaseContext context, T entityToUpdate, T entity) where T : IEntityWithEvent
        {
            if (EventIdIsDifferent(entityToUpdate, entity))
            {
                var newEvent = context.Events.Find(entity.EventId);
                if (newEvent == null)
                    return;

                entityToUpdate.EventId = entity.EventId;
                entityToUpdate.Event = newEvent;
            }
        }

        public static void UpdateEvents(DatabaseContext context, ICollection<Event> @events, ICollection<Event> @eventsToUpdate)
        {
            var setA = new HashSet<int>(@events.Select(e => e.Id));
            var setB = new HashSet<int>(@eventsToUpdate.Select(e => e.Id));

            if (!setA.SetEquals(setB))
            {
                foreach (var evt in events)
                {
                    if (!eventsToUpdate.Any(e => e.Id == evt.Id))
                        eventsToUpdate.Add(evt);
                }

                var toDelete = FindEventsToDelete(@eventsToUpdate, @events);
                foreach (var evt in toDelete)
                {
                    @eventsToUpdate.Remove(evt);
                }
            }

        }

        public static void UpdateEvent(DatabaseContext context, Event @event, Event eventToUpdate)
        {
            if (@event != null && eventToUpdate != null)
            {
                eventToUpdate.Title = @event.Title;
                eventToUpdate.EntryFee = @event.EntryFee;
                eventToUpdate.StartingDate = @event.StartingDate;
                eventToUpdate.Description = @event.Description;
                eventToUpdate.EndDate = @event.EndDate;
                eventToUpdate.TotalVacancies = @event.TotalVacancies;
                eventToUpdate.AvaiableVacancies = @event.AvaiableVacancies;

                InscriptionMethods.UpdateInscription(context, @event.Inscriptions, eventToUpdate.Inscriptions);
                CategoryMethods.UpdateCategoryId(context, eventToUpdate, @event);
                DayMethods.UpdateDay(context, @event.Days, eventToUpdate.Days);
                OrganizerMethods.UpdateOrganizerId(context, eventToUpdate, @event);
            }
            else if (eventToUpdate == null && @event != null)
            {
                if (EventExists(context, @event))
                    context.Events.Attach(@event);
            }

        }

        public static void UpdateEventBase(DatabaseContext context, EventDto eventDto, Event eventToUpdate)
        {
            if (eventDto != null && eventToUpdate != null)
            {
                eventToUpdate.Title = eventDto.Title;
                eventToUpdate.EntryFee = eventDto.EntryFee;
                eventToUpdate.StartingDate = eventDto.StartingDate;
                eventToUpdate.Description = eventDto.Description;
                eventToUpdate.EndDate = eventDto.EndDate;
                eventToUpdate.TotalVacancies = eventDto.TotalVacancies;
                eventToUpdate.AvaiableVacancies = eventDto.AvaiableVacancies;
            }
            else if (eventToUpdate == null && eventDto != null)
            {
                if (EventExists(context, eventDto))
                {
                    var @event = context.Events.FirstOrDefault(e =>
                        e.CategoryId == eventDto.CategoryId &&
                        e.Title == eventDto.Title &&
                        e.OrganizerId == eventDto.OrganizerId &&
                        e.StartingDate == eventDto.StartingDate &&
                        e.EndDate == eventDto.EndDate
                    );
                    context.Events.Attach(@event!);
                }
            }

        }
        
        public static Event CreateEvent(DatabaseContext context, EventDto eventDto)
        {
            if (eventDto == null)
                throw new ArgumentNullException("Error in CreateEvent - EventDto Received is Null");

            var @event = new Event()
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                TotalVacancies = eventDto.TotalVacancies,
                AvaiableVacancies = eventDto.AvaiableVacancies,
                StartingDate = eventDto.StartingDate,
                EndDate = eventDto.EndDate,
                OrganizerId = eventDto.OrganizerId,
                CategoryId = eventDto.CategoryId
            };

            context.Events.Add(@event);

            return @event;
        }
        
    }
}