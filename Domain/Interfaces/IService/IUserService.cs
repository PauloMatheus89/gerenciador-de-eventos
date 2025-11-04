using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IUserService
    {
        void AddUser(User user);
        void RemoveUser(User user);
        void UpdateUser(int id,User user);
        User? GetById(int userId);
        bool VerifyPassword(int userId, string password);
        void ResetPassord(int userId, string password, string newPassword);
        bool ExistsByEmail(string email);
    }
}