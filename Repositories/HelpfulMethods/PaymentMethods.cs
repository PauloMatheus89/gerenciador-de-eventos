using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IEntities;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class PaymentMethods
    {
        public static List<Payment> FindPaymentsToDelete(ICollection<Payment> presentList, ICollection<Payment> newList)
        {
            return presentList.Where(e => !newList.Any(pay => e.Id == pay.Id)).ToList();
        }
        public static bool PaymentIdIsDifferent<T>(T entityToUpdate, T entity) where T : IEntityWithPayment
        {
            return entityToUpdate.PaymentId != 0 && entityToUpdate.PaymentId != entity.PaymentId;
        }

        public static void UpdatePaymentId<T>(DatabaseContext context, T entityToUpdate, T entity) where T : IEntityWithPayment
        {
            if (PaymentIdIsDifferent(entityToUpdate, entity))
            {
                var newPayment = context.Payments.Find(entity.PaymentId);
                if (newPayment == null)
                    return;

                entityToUpdate.PaymentId = entity.PaymentId;
                entityToUpdate.Payment = newPayment;
            }
        }

        public static void UpdatePayment(DatabaseContext context, ICollection<Payment> payments, ICollection<Payment> paymentsToUpdate)
        {
            var setA = new HashSet<int>(payments.Select(e => e.Id));
            var setB = new HashSet<int>(paymentsToUpdate.Select(e => e.Id));

            if (!setA.SetEquals(setB))
            {
                foreach (var pay in payments)
                {
                    if (!paymentsToUpdate.Any(e => e.Id == pay.Id))
                        paymentsToUpdate.Add(pay);
                }

                var toDelete = FindPaymentsToDelete(paymentsToUpdate, payments);
                foreach (var pay in toDelete)
                {
                    paymentsToUpdate.Remove(pay);
                }
            }

        }
    }
}