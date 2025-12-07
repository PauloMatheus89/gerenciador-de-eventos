using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IInscriptionService
    {
        Task<Inscription> AddInscription(InscriptionDTO inscriptionDTO);
        Task<Inscription> RemoveInscription(int id);
        Task<Inscription> UpdateInscription(int id, InscriptionDTO inscriptionDTO);
        Task<Inscription?> GetById(int inscriptionId);
        Task<IEnumerable<Inscription>> GetAllInscriptions();
    }
}