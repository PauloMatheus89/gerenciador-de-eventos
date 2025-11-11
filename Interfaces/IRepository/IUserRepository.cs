using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IUserRepository
    {
        void Create(User user);
        void Update(int id, User user);
        void Delete(User user);
        User? GetByid(int id);
        List<User> GetAllUsers();
    }
}