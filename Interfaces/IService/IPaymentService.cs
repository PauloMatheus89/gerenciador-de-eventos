using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IPaymentService
    {
        Task<Payment> AddPayment(PaymentDto paymentDto);
        Task<Payment> RemovePayment(int id);
        Task<Payment> UpdatePayment(int id,PaymentDtoWithId paymentDto);
        Task<Payment?> GetById(int paymentId);
        Task<IEnumerable<Payment>> GetPayments();
        Task<bool> PaymentExists(int id);
    }
}