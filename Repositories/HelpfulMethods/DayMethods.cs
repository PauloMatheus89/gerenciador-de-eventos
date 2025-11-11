using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IEntities;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class DayMethods
    {
        public static ICollection<Day> FindDaysToDelete(ICollection<Day> presentList, ICollection<Day> newList)
        {
            return presentList.Where(e => !newList.Any(evt => e.Id == evt.Id)).ToList();
        }

        public static bool DayIdIsDifferent(int dayToUpdateId,int dayId) 
        {
            return dayId != 0 && dayToUpdateId != dayId;
        }

        public static void UpdateDayId<T>(DatabaseContext context, T entityToUpdate, T entity) where T : IEntityWithDay
        {
            if (DayIdIsDifferent(entityToUpdate.DayId, entity.DayId))
            {
                var newDay = context.Days.Find(entity.DayId);
                if (newDay == null)
                    return;

                entityToUpdate.DayId = entity.DayId;
                entityToUpdate.Day = newDay;
            }
        }

        public static void UpdateDay(DatabaseContext context, ICollection<Day> days, ICollection<Day> daysToUpdate)
        {
            var setA = new HashSet<int>(days.Select(e => e.Id));
            var setB = new HashSet<int>(daysToUpdate.Select(e => e.Id));

            if (!setA.SetEquals(setB))
            {
                foreach (var day in days)
                {
                    if (!daysToUpdate.Any(e => e.Id == day.Id))
                        daysToUpdate.Add(day);
                }

                var toDelete = FindDaysToDelete(daysToUpdate, days);
                foreach (var day in toDelete)
                {
                    daysToUpdate.Remove(day);
                }
            }

        }

        public static bool DayExists(DatabaseContext context, Day? day)
        {
            if (day != null && context.Days.Any(a => a.Id == day.Id))
                return true;

            return false;
        }

        public static void UpdateDay(DatabaseContext context, Day? day, Day? dayToUpdate)
        {
            if (day != null && dayToUpdate != null)
            {
                dayToUpdate.OpeningTime = day.OpeningTime;
                dayToUpdate.ClosingTime = day.ClosingTime;
                dayToUpdate.Description = day.Description;
                dayToUpdate.Date = day.Date;
                EventMethods.UpdateEventId(context, dayToUpdate, day);
                ActivityMethods.UpdateActivity(context, day.Activities, dayToUpdate.Activities);

            }
            else if (dayToUpdate == null && day != null)
            {
                if (DayExists(context, day))
                    context.Days.Attach(day);
            }

        }
    }
}