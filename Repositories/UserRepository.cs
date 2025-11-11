using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace GerenciadorEventos.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User? GetByid(int id)
        {
            var user = _context.Users
                        .Include(u => u.Participant)
                        .Include(u => u.Organizer)
                        .Include(u => u.Payments)
                        .Include(u => u.Favorites)
                        .Include(u => u.Inscriptions)
                        .FirstOrDefault(u => u.Id == id);

            return user;
        }

        
        public void Update(int id, User user)
        {
            var updateUser = _context.Users
                                .Include(e => e.Organizer)
                                .Include(e => e.Participant)
                                .Include(e => e.Inscriptions)
                                .Include(e => e.Payments)
                                .Include(e => e.Favorites)
                                .FirstOrDefault(e => e.Id == id);



            if(user == null)
            {
                throw new NullReferenceException("User cannot be null");
            }

            if (updateUser == null)
            {
                Console.WriteLine("User was not Found!");
            }
            else
            {
                if (updateUser.Organizer == null)
                {
                    if (updateUser.Participant != null && user.Participant != null)
                    {
                        updateUser.Participant.Email = user.Participant.Email;
                        updateUser.Participant.Name = user.Participant.Name;
                    }
                    else if (updateUser.Participant == null)
                    {
                        if (user.Participant != null && _context.Participants.Any(e => e.Id == user.Participant.Id))
                        {
                            _context.Participants.Attach(user.Participant);
                        }
                        updateUser.Participant = user.Participant;
                    }
                }

                if (updateUser.Participant == null)
                {
                    if (updateUser.Organizer != null && user.Organizer != null)
                    {
                        updateUser.Organizer.CorporateEmail = user.Organizer.CorporateEmail;
                        updateUser.Organizer.CorporateName = user.Organizer.CorporateName;
                        //TODO FAZER VALIDAÇÃO PARA EVITAR CRIAÇÃOD E NOVAS ENTIDADES
                        updateUser.Organizer.Address = user.Organizer.Address;
                        updateUser.Organizer.Description = user.Organizer.Description;
                        updateUser.Organizer.Document = user.Organizer.Document;
                        //TODO FAZER VALIDAÇÃO PARA EVITAR CRIAÇÃO DE NOVAS ENTIDADES
                        updateUser.Organizer.Events = user.Organizer.Events;
                    }
                    else if (updateUser.Organizer == null)
                    {
                        if (user.Organizer != null && _context.Organizers.Any(e => e.Id == user.Organizer.Id))
                        {
                            _context.Organizers.Attach(user.Organizer);
                        }
                        updateUser.Organizer = user.Organizer;
                    }
                }

                updateUser.Username = user.Username;
                updateUser.Email = user.Email;
                updateUser.Role = user.Role;

                if (updateUser.Favorites != user.Favorites)
                {
                    foreach (var fav in user.Favorites)
                    {
                        if (!_context.Favorites.Any(e => e.Id == fav.Id))
                            _context.Favorites.Add(fav);
                    }

                    var toDelete = updateUser.Favorites.Where(e => !user.Favorites.Any(evt => evt.Id == e.Id)).ToList();
                    foreach (var favToDelete in toDelete)
                    {
                        updateUser.Favorites.Remove(favToDelete);
                    }
                }

                if (updateUser.Inscriptions != user.Inscriptions)
                {
                    foreach (var ins in user.Inscriptions)
                    {
                        if (!_context.Inscriptions.Any(e => e.Id == ins.Id))
                            _context.Inscriptions.Add(ins);
                    }

                    var toDelete = updateUser.Inscriptions.Where(e => !user.Inscriptions.Any(evt => evt.Id == e.Id)).ToList();
                    foreach (var insToDelete in toDelete)
                    {
                        updateUser.Inscriptions.Remove(insToDelete);
                    }
                }
                
                if(updateUser.Payments != user.Payments)
                {
                    foreach (var pay in user.Payments)
                    {
                        if (!_context.Payments.Any(e => e.Id == pay.Id))
                            _context.Payments.Add(pay);
                    }
                    
                    var toDelete = updateUser.Payments.Where(e => !user.Payments.Any(evt => evt.Id == e.Id)).ToList();
                    foreach(var payToDelete in toDelete)
                    {
                        updateUser.Payments.Remove(payToDelete);
                    }
                }

                _context.SaveChanges();
            }


        }


        public List<User> GetAllUsers()
        {
            return _context.Users.
                    Include(e => e.Participant)
                    .Include(e => e.Favorites)
                    .Include(e => e.Organizer)
                    .Include(e => e.Payments)
                    .Include(e => e.Payments).ToList();
        }
        
    }
}