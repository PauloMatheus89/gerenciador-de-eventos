using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IOrganizerService
    {
        Task<Organizer> AddOrganizer(OrganizerDto organizerDto);
        Task<Organizer> RemoveOrganizer(int id);
        Task<Organizer> UpdateOrganizer(int id,OrganizerDto organizerDto);
        Task<Organizer?> GetById(int organizerId);
        Task<IEnumerable<Organizer>> GetOrganizers();
        Task<bool> DocumentExists(string document);
        Task<bool> CorporateEmailExists(string corporateEmail);

    }
}