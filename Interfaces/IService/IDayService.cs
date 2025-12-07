using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IDayService
    {
        Task<Day> AddDay(DayDTO Day);
        Task<Day> DeleteDay(int id);
        Task<Day> UpdateDay(int id, DayDTO newDay);
        Task<Day?> GetDay(int id);
        Task<IEnumerable<Day>> GetDays();


        
    }
}