using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IEntities;
using GerenciadorEventos.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

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

        public static bool UserExists(DatabaseContext context, UserDto userDto)
        {
            if (userDto != null && context.Users.Any(a => a.Email == userDto.Email))
                return true;

            return false;
        }

        public static bool UserIdIsDifferent<TEntity, TUserId>(TEntity entityToUpdate,TEntity entity) where TEntity : IEntityWithUser<TUserId>
        {
            return !EqualityComparer<TUserId>.Default.Equals(entityToUpdate.UserId, default)
                && !EqualityComparer<TUserId>.Default.Equals(entityToUpdate.UserId, entity.UserId);
        }

        public static void UpdateUserId<TEntity, TUserId>(DatabaseContext context,TEntity entityToUpdate,TEntity entity) where TEntity : IEntityWithUser<TUserId>
        {
            if (UserIdIsDifferent<TEntity, TUserId>(entityToUpdate, entity))
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

        public static void UpdateUserBase(DatabaseContext context, UserDto? userDto, User? userToUpdate)
        {
            if (userDto != null && userToUpdate != null)
            {
                userToUpdate.Email = userDto.Email;
                userToUpdate.Password = userDto.Password;
                userToUpdate.Username = userDto.Username;
                userToUpdate.Role = userDto.Role;
            }
            else if (userToUpdate == null && userDto != null)
            {
                if (UserExists(context, userDto))
                {
                    var user = context.Users.FirstOrDefault(e => e.Email == userDto.Email);
                    context.Users.Attach(user!);
                }
            }
        }

        public static User CreateUser(DatabaseContext context, UserDto userDto)
        {
            var user = new User()
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Email = userDto.Email,
                Role = userDto.Role,
            };

            context.Users.Add(user);

            return user;
        }

        
    }
}