using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<User?> GetByid(string id);
        Task<User?> GetByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task<bool> EmailExists(string email);
        Task<bool> UserExists(string id);


    }
}