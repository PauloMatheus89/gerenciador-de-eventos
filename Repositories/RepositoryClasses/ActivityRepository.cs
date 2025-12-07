using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories.RepositoryClasses
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly DatabaseContext _context;

        public ActivityRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Activity> Create(Activity activity)
        {
           await _context.Activities.AddAsync(activity);
           await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<IEnumerable<Activity>> GetAllActivity()
        {
            return await _context.Activities
                    .Include(e => e.Day)
                    .ToListAsync();
        }

        public async Task<Activity?> GetById(int id)
        {
            return await _context.Activities
                    .Include(e => e.Day)
                    .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Activity> Remove(Activity activity)
        {
            _context.Activities.Remove(activity);
           await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<Activity> Update(Activity activityToUpdate, Activity newActivity)
        {
            activityToUpdate.Title = newActivity.Title;
            activityToUpdate.StartTime = newActivity.StartTime;
            activityToUpdate.EndTime = newActivity.EndTime;
            activityToUpdate.Description = newActivity.Description;

           await _context.SaveChangesAsync();

            return activityToUpdate;
        }
    }
}