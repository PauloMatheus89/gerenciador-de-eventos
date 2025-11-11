using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IInscriptionRepository
    {
        void Create(Inscription inscription);
        void Remove(Inscription inscription);
        void Update(int id, Inscription inscription);
        Inscription GetById(int id);
    }
}