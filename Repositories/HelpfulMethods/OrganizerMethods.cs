using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using GerenciadorEventos.Domain.Models.DTOs;
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

        public static bool OrganizerExists(DatabaseContext context, OrganizerDto? organizerDto)
        {
            if (organizerDto != null && context.Organizers.Any(a => a.Document == organizerDto.Document))
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
                EventMethods.UpdateEvents(context, organizer.Events, organizerToUpdate.Events);
                UserMethods.UpdateUserId<Organizer, int>(context, organizerToUpdate, organizer);
            }
            else if (organizerToUpdate == null && organizer != null)
            {
                if (OrganizerExists(context, organizer))
                    context.Organizers.Attach(organizer);
            }
        }
        
        public static void UpdateOrganizerBase(DatabaseContext context, OrganizerDto? organizerDto, Organizer? organizerToUpdate)
        {
            if (organizerDto != null && organizerToUpdate != null)
            {
                organizerToUpdate.CorporateEmail = organizerDto.CorporateEmail;
                organizerToUpdate.CorporateName = organizerDto.CorporateName;
                organizerToUpdate.Description = organizerDto.Description;
                organizerToUpdate.Document = organizerDto.Document;
            }
            else if (organizerToUpdate == null && organizerDto != null)
            {
                if (OrganizerExists(context, organizerDto))
                {
                    var organizer = context.Organizers.FirstOrDefault(e => e.Document == organizerDto.Document);

                    context.Organizers.Attach(organizer!);
                }
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

        public static Organizer CreateOrganizer(OrganizerDto dto,DatabaseContext context)
        {
            var user = context.Users.Find(dto.UserId);

            if (user == null)
            {
                throw new ArgumentNullException("O valor referente ao Organizer Id não corresponde com nenhum User Válido!");
            }

            if (user.Organizer != null)
            {
                throw new Exception("O usuário referente a este Organizer Id já está associado com outro organizador");
            }

            var organizer = new Organizer()
            {
                CorporateEmail = dto.CorporateEmail,
                CorporateName = dto.CorporateName,
                Document = dto.Document,
                Description = dto.Description,
                UserId = dto.UserId,
                User = user
            };

            context.Organizers.Add(organizer);

            return organizer;
        }
    }
}