using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IPaymentRepository
    {
        Task<Payment> Create(Payment payment);
        Task<Payment> Remove(Payment payment);
        Task<Payment> Update(Payment paymentToUpdate, Payment newPayment);
        Task<Payment?> GetById(int id);
        Task<ICollection<Payment>> GetAllPayments();
        Task<bool> PaymentExists(int id);
    }
}