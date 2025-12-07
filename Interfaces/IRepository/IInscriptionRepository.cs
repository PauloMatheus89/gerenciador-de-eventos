using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IInscriptionRepository
    {
        Task<Inscription> Create(Inscription inscription);
        Task<Inscription> Remove(Inscription inscription);
        Task<Inscription> Update(Inscription inscriptionToUpdate, Inscription inscription);
        Task<Inscription?> GetById(int id);
        Task<IEnumerable<Inscription>> GetAllInscriptions();
    }
}