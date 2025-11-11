using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IPaymentRepository
    {
        void Create(Payment payment);
        void Remove(Payment payment);
        void Update(int id, Payment payment);
        Payment GetById(int id);
    }
}