using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IActivityRepository
    {
        Task<Activity> Create(Activity activity);
        Task<Activity> Remove(Activity activity);
        Task<Activity> Update(Activity activityToUpdate, Activity newActivity);
        Task<Activity?> GetById(int id);
        Task<IEnumerable<Activity>> GetAllActivity();
    }
}