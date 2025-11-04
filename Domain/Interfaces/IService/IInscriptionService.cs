using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IInscriptionService
    {
        void AddInscription(Inscription inscription);
        void RemoveInscription(Inscription inscription);
        void UpdateInscription(int id,Inscription inscription);
        Inscription? GetById(int inscriptionId);
    }
}