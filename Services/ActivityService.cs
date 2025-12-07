using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;

namespace GerenciadorEventos.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        public async Task<Activity> AddActivity(ActivityDTO activityDTO)
        {
            if (activityDTO is null)
                throw new ArgumentNullException("ActivityDTO received is null!");

            var newActivity = new Activity
            {
                Title = activityDTO.Title,
                StartTime = activityDTO.StartTime,
                EndTime = activityDTO.EndTime,
                Description = activityDTO.Description,
                DayId = activityDTO.DayId
            };

            var activity = await _activityRepository.Create(newActivity);

            return activity;
        }

        public async Task<IEnumerable<Activity>> GetActivities()
        {
            return await _activityRepository.GetAllActivity();
        }

        public async Task<Activity?> GetActivity(int id)
        {
            return await _activityRepository.GetById(id);
        }

        public async Task<Activity> RemoveActivity(int id)
        {
            var activityToRemove = await _activityRepository.GetById(id);
 
            if (activityToRemove is null)
                throw new KeyNotFoundException("Could Not Find a Activity related to That Id");

            var activity = await _activityRepository.Remove(activityToRemove);

            return activity;
        }

        public async Task<Activity> UpdateActivity(int id, ActivityDTO activityDTO)
        {
            var activityToUpdate = await _activityRepository.GetById(id);

            if (activityToUpdate is null)
                throw new KeyNotFoundException("Could Not Find a Activity related to That Id");

            if (activityDTO is null)
                throw new ArgumentNullException("ActivityDTO received is null!");

            var newActivity = new Activity
            {
                Title = activityDTO.Title,
                StartTime = activityDTO.StartTime,
                EndTime = activityDTO.EndTime,
                Description = activityDTO.Description
            };

            var activity = await _activityRepository.Update(activityToUpdate, newActivity);

            return activity;
        }
    }
}