using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Enums;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories.RepositoryClasses
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DatabaseContext _context;

        public PaymentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Payment> Create(Payment payment)
        {
        
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<ICollection<Payment>> GetAllPayments()
        {
            return await _context.Payments
                    .Include(e => e.Inscription)
                    .Include(e => e.User)
                    .ToListAsync();
        }

        public async Task<Payment?> GetById(int id)
        {
            return await _context.Payments
                    .Include(e => e.Inscription)
                    .Include(e => e.User)
                    .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Payment> Remove(Payment payment)
        {
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return payment!;
        }

        public async Task<Payment> Update(Payment paymentToUpdate, Payment newPayment)
        {

            paymentToUpdate.Value = newPayment.Value;
            paymentToUpdate.Status = newPayment.Status;
            paymentToUpdate.PaymentDate = newPayment.PaymentDate;
            paymentToUpdate.PaymentMethod = newPayment.PaymentMethod;

            await _context.SaveChangesAsync();

            return paymentToUpdate;

        }
        
        public async Task<bool> PaymentExists(int id)
        {
            return await _context.Payments.AnyAsync(e => e.Id == id);
        }

    }
}