using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IActivityService
    {
        Task<Activity> AddActivity(ActivityDTO activityDTO);
        Task<Activity> RemoveActivity(int id);
        Task<Activity> UpdateActivity(int id, ActivityDTO activityDTO);
        Task<Activity?> GetActivity(int id);
        Task<IEnumerable<Activity>> GetActivities();

        
    }
}