using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Services
{
    public class PaymentService : IPaymentService
    {

        //TODO VERIFICAR SE EXISTEM ORGANIZERS ATVIOS
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> AddPayment(PaymentDto paymentDto)
        {
            if (paymentDto is null)
                throw new ArgumentNullException(nameof(paymentDto));

            if (paymentDto.PaymentDate > DateTime.Now)
                throw new ArgumentException("A data de pagamento inválida");

            var newPayment = new Payment
            {
                PaymentDate = paymentDto.PaymentDate,
                PaymentMethod = paymentDto.PaymentMethod,
                Value = paymentDto.Value,
                Status = paymentDto.Status
            };

            var payment = await _paymentRepository.Create(newPayment);

            return payment;
        }

        public async Task<Payment?> GetById(int paymentId)
        {
            return await _paymentRepository.GetById(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetPayments()
        {
            return await _paymentRepository.GetAllPayments();
        }

        public async Task<Payment> RemovePayment(int id)
        {
            var paymentToDelete = await _paymentRepository.GetById(id);

            if (paymentToDelete is null)
                throw new KeyNotFoundException("Pagamento não encontrado");

            var payment = await _paymentRepository.Remove(paymentToDelete);

            return payment;
        }

        //TODO : REVISTAR ESSA LOGICA DE DTO COM ID
        public async Task<Payment> UpdatePayment(int id, PaymentDtoWithId paymentDtoWithId)
        {
            var paymentToUpdate = await _paymentRepository.GetById(id);

            if (paymentToUpdate is null)
                throw new KeyNotFoundException("Payment was not found");

            if (paymentDtoWithId is null)
                throw new ArgumentNullException("Dto received is null");

            if (paymentDtoWithId.Id != id && await _paymentRepository.PaymentExists(paymentDtoWithId.Id))
                throw new DuplicateNameException("Trying to add a duplicated payment to the database");

            var newPayment = new Payment
            {
                Value = paymentDtoWithId.Value,
                Status = paymentDtoWithId.Status,
                PaymentDate = paymentDtoWithId.PaymentDate,
                PaymentMethod = paymentDtoWithId.PaymentMethod
            };

            var payment = await _paymentRepository.Update(paymentToUpdate, newPayment);

            return payment;
        }
        
        public async Task<bool> PaymentExists(int id)
        {
            return await _paymentRepository.PaymentExists(id);
        }
        
    }
}