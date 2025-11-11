using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IEntities;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class UserMethods
    {
        public static bool UserExists(DatabaseContext context, User user)
        {
            if (user != null && context.Users.Any(a => a.Id == user.Id))
                return true;

            return false;
        }

        public static bool UserIdIsDifferent<T>(T entityToUpdate, T entity) where T : IEntityWithUser
        {
            return entityToUpdate.UserId != 0 && entityToUpdate.UserId != entity.UserId;
        }

        public static void UpdateUserId<T>(DatabaseContext context, T entityToUpdate, T entity) where T : IEntityWithUser
        {
            if (UserIdIsDifferent(entityToUpdate, entity))
            {
                var newUser = context.Users.Find(entity.UserId);
                if (newUser == null)
                    return;

                entityToUpdate.UserId = entity.UserId;
                entityToUpdate.User = newUser;
            }
        }

        public static void UpdateUser(DatabaseContext context, User? user, User? userToUpdate)
        {
            if (user != null && userToUpdate != null)
            {
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Username = user.Username;
                userToUpdate.Role = user.Role;

                OrganizerMethods.UpdateOrganizer(context, user.Organizer, userToUpdate.Organizer);
                ParticipantMethods.UpdateParticipant(context, user.Participant, userToUpdate.Participant);
                InscriptionMethods.UpdateInscription(context, user.Inscriptions, userToUpdate.Inscriptions);
                PaymentMethods.UpdatePayment(context, user.Payments, userToUpdate.Payments);
                FavoriteMethods.UpdateFavorite(context, user.Favorites, userToUpdate.Favorites);

            }
            else if (userToUpdate == null && user != null)
            {
                if (UserExists(context, user))
                    context.Users.Attach(user);
            }
        }
    }
}