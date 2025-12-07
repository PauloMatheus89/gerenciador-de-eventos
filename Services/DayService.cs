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
    public class DayService : IDayService
    {
        //TODO: VALIDAÇÃO PARA VERIFICAR SE UM EVENTO NÃO POSSUI DOIS DAYS NA MESMA DATA
        private readonly IDayRepository _dayRepository;

        public DayService(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        public async Task<Day> AddDay(DayDTO dayDTO)
        {
            if (dayDTO is null)
                throw new ArgumentNullException("DayDto Received is null");

            var day = new Day
            {
                Date = dayDTO.Date,
                OpeningTime = dayDTO.OpeningTime,
                ClosingTime = dayDTO.ClosingTime,
                Description = dayDTO.Description
            };

            var newDay = await _dayRepository.Create(day);

            return newDay;
        }

        public async Task<Day> DeleteDay(int id)
        {
            var dayToDelete = await _dayRepository.GetById(id);

            if (dayToDelete is null)
                throw new KeyNotFoundException("Could not find a Day associated with that Id");

            var day = await _dayRepository.Remove(dayToDelete);

            return dayToDelete;
        }

        public async Task<Day?> GetDay(int id)
        {
            return await _dayRepository.GetById(id);
        }

        public async Task<IEnumerable<Day>> GetDays()
        {
            return await _dayRepository.GetAllDays();
        } 

        public async Task<Day> UpdateDay(int id, DayDTO dayDTO)
        {
            var dayToUpdate = await _dayRepository.GetById(id);

            if (dayToUpdate is null)
                throw new KeyNotFoundException("Could not find a Day associated with that Id");

            if (dayDTO is null)
                throw new ArgumentException("DayDTO received is null!");

            var newDay = new Day
            {
                Date = dayDTO.Date,
                OpeningTime = dayDTO.OpeningTime,
                ClosingTime = dayDTO.ClosingTime,
                Description = dayDTO.Description
            };

            var day = await _dayRepository.Update(dayToUpdate, newDay);

            return day;

        }
    }
}