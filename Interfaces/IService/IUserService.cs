using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.DTOs.UserDTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IUserService
    {
        Task<User> AddUser(UserCreateDto dto);
        Task<User> RemoveUserAsync(string id);
        Task<User> UpdateUser(string id,UserUpdateDTO userDto);
        Task<User?> GetById(string userId);
        Task<bool> VerifyPassword(string userId, string password);
        void ResetPassord(string userId, string password, string newPassword);
        Task<bool> ExistsByEmail(string email);
        Task<bool> UserExists(string id);
    }
}