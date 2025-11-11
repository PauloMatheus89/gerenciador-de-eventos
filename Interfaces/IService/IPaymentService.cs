using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IPaymentService
    {
        void AddPayment(Payment payment);
        void RemovePayment(Payment payment);
        void UpdatePayment(int id,Payment payment);
        Payment? GetById(int paymentId);
    }
}