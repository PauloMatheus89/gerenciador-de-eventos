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
            var user = _context.Users.Find();

            return user;
        }

        
        public void Update(int id, User user)
        {
            var updateUser = _context.Users.Find(id);

            if (updateUser == null)
            {
                Console.WriteLine("User was not Found!");
            }
            else
            {
                updateUser.Username = user.Username;
                updateUser.Email = user.Email;
                updateUser.Role = user.Role;

                _context.SaveChanges();
            }


        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include(e => e.Organizer).ToList();
        }
        
    }
}