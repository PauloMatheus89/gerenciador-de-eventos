using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Infrastructure.Databases;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class ActivityMethods
    {
        public static ICollection<Activity> FindActivitiesToDelete(ICollection<Activity> presentList, ICollection<Activity> newList)
        {
            return presentList.Where(e => !newList.Any(evt => e.Id == evt.Id)).ToList();
        }
        
        public static void UpdateActivity(DatabaseContext context, ICollection<Activity> activities, ICollection<Activity> activitiesToUpdate)
        {
            var setA = new HashSet<int>(activities.Select(e => e.Id));
            var setB = new HashSet<int>(activitiesToUpdate.Select(e => e.Id));

            if (!setA.SetEquals(setB))
            {
                foreach (var act in activities)
                {
                    if (!activitiesToUpdate.Any(e => e.Id == act.Id))
                        activitiesToUpdate.Add(act);
                }

                var toDelete = FindActivitiesToDelete(activitiesToUpdate, activities);
                foreach (var evt in toDelete)
                {
                    activitiesToUpdate.Remove(evt);
                }
            }

        }
    }
}