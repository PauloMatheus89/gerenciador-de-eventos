using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;

namespace GerenciadorEventos.Models.Services
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IOrganizerRepository _organizerRepository;

        public OrganizerService(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        public void AddOrganizer(Organizer organizer)
        {
            if (organizer == null)
            {
                throw new NullReferenceException("Error!Trying to add null organizer to database");
            }

            _organizerRepository.Create(organizer);
        }

        public Organizer? GetById(int organizerId)
        {
            var organizer = _organizerRepository.GetByid(organizerId);

            if (organizer == null)
            {
                throw new NullReferenceException("Organizer cannot be null!");
            }

            return organizer;
        }

        public void RemoveOrganizer(Organizer organizer)
        {
            if (organizer == null)
            {
                throw new NullReferenceException(nameof(organizer));
            }

            _organizerRepository.Delete(organizer);
        }

        public void UpdateOrganizer(int id, Organizer organizer)
        {
            if (organizer == null)
            {
                throw new NullReferenceException("Error! Organizer Cannot be null");
            }

            _organizerRepository.Update(id, organizer);
        }
    }
}