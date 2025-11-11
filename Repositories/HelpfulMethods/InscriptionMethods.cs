using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class InscriptionMethods
    {
        public static bool InscriptionExists(DatabaseContext context, Inscription? inscription)
        {
            if (inscription != null && context.Inscriptions.Any(a => a.Id == inscription.Id))
                return true;

            return false;
        }

        public static ICollection<Inscription> FindInscriptionsToDelete(ICollection<Inscription> presentList, ICollection<Inscription> newList)
        {
            return presentList.Where(e => !newList.Any(evt => e.Id == evt.Id)).ToList();
        }

        public static void UpdateInscription(DatabaseContext context, object inscriptions, object inscriptionsToUpdate)
        {
            // --- Caso seja lista de inscrições ---
            if (inscriptionsToUpdate is ICollection<Inscription> listToUpdate && inscriptions is ICollection<Inscription> newList)
            {
                var setA = new HashSet<int>(newList.Select(e => e.Id));
                var setB = new HashSet<int>(listToUpdate.Select(e => e.Id));

                if (!setA.SetEquals(setB))
                {
                    // Adiciona novos registros
                    foreach (var ins in newList)
                    {
                        if (!listToUpdate.Any(e => e.Id == ins.Id))
                            listToUpdate.Add(ins);
                    }

                    // Remove registros que não existem mais
                    var toDelete = FindInscriptionsToDelete(listToUpdate, newList);
                    foreach (var insToDelete in toDelete)
                    {
                        listToUpdate.Remove(insToDelete);
                    }
                }
            }
            // --- Caso seja uma única inscrição ---
            else if (inscriptionsToUpdate is Inscription inscriptionToUpdate && inscriptions is Inscription inscription)
            {
                if (inscription != null && inscriptionToUpdate != null)
                {
                    inscriptionToUpdate.InscriptionDate = inscription.InscriptionDate;
                    inscriptionToUpdate.Status = inscription.Status;

                    // Atualiza chaves estrangeiras
                    EventMethods.UpdateEventId(context, inscriptionToUpdate, inscription);
                    UserMethods.UpdateUserId(context, inscriptionToUpdate, inscription);
                    PaymentMethods.UpdatePaymentId(context, inscriptionToUpdate, inscription);

                    
                }
                else if (inscriptionToUpdate == null && inscription != null)
                {
                    if (InscriptionExists(context, inscription))
                        context.Inscriptions.Attach(inscription);
                }
            }
        }
    }
}