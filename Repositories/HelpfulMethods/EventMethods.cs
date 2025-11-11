using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IEntities;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class EventMethods
    {
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

        public static void UpdateEvent(DatabaseContext context, ICollection<Event> @events, ICollection<Event> @eventsToUpdate)
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
    }
}