using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IOrganizerRepository
    {
        Task<Organizer> Create(Organizer organizer);
        Task<Organizer> Delete(Organizer organizer);
        Task<Organizer> Update(Organizer organizerToUpdate, Organizer newOrganizer);
        Task<Organizer?> GetByid(int? id);
        Task<Organizer?> GetByDocument(string document);
        Task<Organizer?> GetByCorporateEmail(string corporateEmail);
        Task<bool> DocumentExists(string document);
        Task<bool> CorporateEmailExists(string corporateEmail);
        Task<IEnumerable<Organizer>> GetAllOrganizers();
    }
}