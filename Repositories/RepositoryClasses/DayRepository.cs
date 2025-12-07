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
    public class DayRepository : IDayRepository
    {
        private readonly DatabaseContext _context;

        public DayRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Day> Create(Day day)
        {
            await _context.Days.AddAsync(day);
            await _context.SaveChangesAsync();

            return day;
        }

        public async Task<IEnumerable<Day>> GetAllDays()
        {
            return await _context.Days
                    .Include(e => e.Address)
                    .Include(e => e.Activities)
                    .Include(e => e.Event)
                    .ToListAsync();
        }

        public async Task<Day?> GetById(int id)
        {
            return await _context.Days
                    .Include(e => e.Address)
                    .Include(e => e.Activities)
                    .Include(e => e.Event)
                    .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Day> Remove(Day day)
        {
            _context.Days.Remove(day);
            await _context.SaveChangesAsync();

            return day;
        }

        public async Task<Day> Update(Day dayToUpdate, Day newDay)
        {
            dayToUpdate.ClosingTime = newDay.ClosingTime;
            dayToUpdate.OpeningTime = newDay.OpeningTime;
            dayToUpdate.Date = newDay.Date;
            dayToUpdate.Description = newDay.Description;

            await _context.SaveChangesAsync();

            return dayToUpdate;
        }
    }
}