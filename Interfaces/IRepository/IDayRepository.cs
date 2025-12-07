using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IDayRepository
    {
        Task<Day> Create(Day day);
        Task<Day> Remove(Day day);
        Task<Day> Update(Day dayToUpdate, Day newDay);
        Task<Day?> GetById(int id);
        Task<IEnumerable<Day>> GetAllDays();
    }
}