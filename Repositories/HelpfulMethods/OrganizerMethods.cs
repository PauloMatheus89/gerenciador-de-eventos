using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IEntities;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class OrganizerMethods
    {
        public static bool OrganizerExists(DatabaseContext context, Organizer? organizer)
        {
            if (organizer != null && context.Organizers.Any(a => a.Id == organizer.Id))
                return true;

            return false;
        }

        public static void UpdateOrganizer(DatabaseContext context, Organizer? organizer, Organizer? organizerToUpdate)
        {
            if (organizer != null && organizerToUpdate != null)
            {
                organizerToUpdate.CorporateEmail = organizer.CorporateEmail;
                organizerToUpdate.CorporateName = organizer.CorporateName;
                organizerToUpdate.Description = organizer.Description;
                organizerToUpdate.Document = organizer.Document;
                AddressMethods.UpdateAddress(context, organizer.Address, organizerToUpdate.Address);
                EventMethods.UpdateEvent(context, organizer.Events, organizerToUpdate.Events);
                UserMethods.UpdateUserId(context, organizerToUpdate, organizer);
            }
            else if (organizerToUpdate == null && organizer != null)
            {
                if (OrganizerExists(context, organizer))
                    context.Organizers.Attach(organizer);
            }


        }
        
        public static bool OrganizerIdIsDifferent<T>(T entityToUpdate, T entity) where T : IEntityWithOrganizer
        {
            return entityToUpdate.OrganizerId != 0 && entityToUpdate.OrganizerId != entity.OrganizerId;
        }

        public static void UpdateOrganizerId<T>(DatabaseContext context, T entityToUpdate, T entity) where T : IEntityWithOrganizer
        {
            if (OrganizerIdIsDifferent(entityToUpdate, entity))
            {
                var newDay = context.Organizers.Find(entity.OrganizerId);
                if (newDay == null)
                    return;

                entityToUpdate.OrganizerId = entity.OrganizerId;
                entityToUpdate.Organizer = newDay;
            }
        }
    }
}