using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Infrastructure.Databases;
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

        public async Task<Organizer> AddOrganizer(OrganizerDto organizerDto)
        {
            if (organizerDto == null)
            {
                throw new NullReferenceException("Error!Trying to add null organizer to database");
            }

            if (organizerDto.Document != null && await DocumentExists(organizerDto.Document))
                throw new DuplicateNameException("There is already an Organizer using this Document!");

            if (organizerDto.CorporateEmail != null && await CorporateEmailExists(organizerDto.CorporateEmail))
                throw new DuplicateNameException("There is already an Organizer registered with this Corporate Email!");

            var newOrganizer = new Organizer
            {
                CorporateEmail = organizerDto.CorporateEmail,
                CorporateName = organizerDto.CorporateName,
                Document = organizerDto.Document,
                Description = organizerDto.Description,
                UserId = organizerDto.UserId,
            };

            var organizer = await _organizerRepository.Create(newOrganizer);

            return organizer;
        }

        public async Task<Organizer?> GetById(int organizerId)
        {
            return await _organizerRepository.GetByid(organizerId);
        }

        public async Task<Organizer> RemoveOrganizer(int id)
        {
            var organizerToRemove = await _organizerRepository.GetByid(id);

            if (organizerToRemove is null)
            {
                throw new KeyNotFoundException("Could not find an Organizer associated with that id!");
            }

            // TODO Verificar se esta associado a um User


            var organizer = await _organizerRepository.Delete(organizerToRemove);

            return organizer;

            
        }

        public async Task<Organizer> UpdateOrganizer(int id, OrganizerDto organizerDto)
        {
            var organizerToUpdate = await _organizerRepository.GetByid(id);

            if (organizerDto == null)
            {
                throw new NullReferenceException("Error! Organizer Cannot be null");
            }

            if (organizerToUpdate is null)
                throw new KeyNotFoundException("Could not find an Organizer Associated with that Id");

            var newOrganizer = new Organizer
            {
                CorporateEmail = organizerDto.CorporateEmail,
                CorporateName = organizerDto.CorporateName,
                Document = organizerDto.Document,
                Description = organizerDto.Description
            };

            var organizerUpdated = await _organizerRepository.Update(organizerToUpdate, newOrganizer);

            return organizerUpdated;


        }

        public async Task<bool> DocumentExists(string document)
        {
            return await _organizerRepository.DocumentExists(document);
        }

        public async Task<bool> CorporateEmailExists(string corporateEmail)
        {
            return await _organizerRepository.CorporateEmailExists(corporateEmail);
        }

        public async Task<IEnumerable<Organizer>> GetOrganizers()
        {
            return await _organizerRepository.GetAllOrganizers();
        }
    }
}