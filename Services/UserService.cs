using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.DTOs.UserDTOs;
using GerenciadorEventos.Exceptions;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Models.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        //TODO : Adcionar Codigo para deletar inscrição quando User for Deletado
        public async Task<User> AddUser(UserCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException("O e-mail não pode ser nulo ou vazio.");
            }

            if (dto.Email != null && await ExistsByEmail(dto.Email))
            {
                throw new DuplicateNameException("Esse Email já existe no banco de dados!");
            }

            var newUser = new User
            {
                Email = dto.Email,
                UserName = dto.Username,
                Role = dto.Role
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password!);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return newUser;
        }

        public async Task<User?> GetById(string userId)
        {
            return await _userManager.Users
                    .Include(e => e.Organizer)
                    .Include(e => e.Organizer)
                    .Include(e => e.Favorites)
                    .Include(e => e.Inscriptions)
                    .Include(e => e.Payments)
                    .FirstOrDefaultAsync(e => e.Id == userId);
        }

        public async Task<User> RemoveUserAsync(string id)
        {
            // Busca o usuário
            var userToDelete = await _userManager.Users
                .Include(u => u.Inscriptions)
                .Include(u => u.Participant)
                .Include(u => u.Organizer)
                .Include(u => u.Favorites)
                .Include(u => u.Payments)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userToDelete == null)
                throw new KeyNotFoundException("Could not find a User associated with that Id!");

            // Verifica inscrições ativas
            if (!string.IsNullOrEmpty(userToDelete.Email) &&
                userToDelete.Inscriptions.Any(i =>
                    i.Status == Enums.Status.Pending ||
                    i.Status == Enums.Status.Approved ||
                    i.Status == Enums.Status.Processing))
            {
                throw new ActiveInscriptionException();
            }

            // Deleta o usuário usando UserManager
            var result = await _userManager.DeleteAsync(userToDelete);

            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            // Retorna o objeto que foi deletado
            return userToDelete;
        }


        public void ResetPassord(string userId, string password, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUser(string id, UserUpdateDTO userDto)
        {
            var userToUpdate = await _userManager.FindByIdAsync(id);

            if (userToUpdate == null)
                throw new KeyNotFoundException($"Id {id} does not exist in the database");

            if (userDto == null)
                throw new NullReferenceException(nameof(userDto));

            if (userDto.Email != userToUpdate.Email && await _userManager.Users.AnyAsync(e => e.Email == userDto.Email))
            {
                throw new DuplicateNameException("Esse Email já existe no banco de dados!");
            }

            userToUpdate.Email = userDto.Email;
            userToUpdate.Role = userDto.Role;

            var result = await _userManager.UpdateAsync(userToUpdate);

            if (!result.Succeeded)
                throw new Exception("Falaha ao remover registro!");

            return userToUpdate;
        }

        public async Task<bool> VerifyPassword(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentException("User does not exist!");

            return await _userManager.CheckPasswordAsync(user, password);
            
        }

        public async Task<bool> HasActiveInscriptions(string email)
        {
            var user = await _userManager.Users.Where(u => u.Email == email).Select(u => new { u.Id }).FirstOrDefaultAsync();
            
            if (user == null)
            {
                throw new ArgumentNullException("User was not found!");
            }

            return await _userManager.Users
                .Where(u => u.Id == user.Id)
                .SelectMany(u => u.Inscriptions)
                .AnyAsync(i => i.Status == Enums.Status.Pending ||
                                i.Status == Enums.Status.Approved ||
                                i.Status == Enums.Status.Processing);
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new NullReferenceException("Error! Email cannot be null");
            }

            return await _userManager.Users.AnyAsync(e => e.Email == email);
        }
        
        public async Task<bool> UserExists(string id)
        {
            return await _userManager.Users.AnyAsync(e => e.Id == id);
        }
        
    }
}