using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;

namespace GerenciadorEventos.Models.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("O e-mail não pode ser nulo ou vazio.");
            }
            
            if (ExistsByEmail(user.Email))
            {
                throw new InvalidOperationException("Email já existe!");
            }

            _userRepository.Create(user);
        }

        public bool ExistsByEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new NullReferenceException("Error! Email cannot be null");
            }

            List<User> users = _userRepository.GetAllUsers();

            return users.Any(user => user.Email == email);
        }

        public User? GetById(int userId)
        {
            return _userRepository.GetByid(userId);
        }

        public void RemoveUser(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException(nameof(user));
            }

            _userRepository.Delete(user);
        }

        public void ResetPassord(int userId, string password, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int id,User user)
        {
            if (user == null)
            {
                throw new NullReferenceException(nameof(user));
            }

            if (user.Inscriptions.Any())
                throw new InvalidOperationException("Não é possível excluir um usuário com inscrições ativas.");

            _userRepository.Update(id, user);
        }

        public bool VerifyPassword(int userId, string password)
        {
            var user = _userRepository.GetByid(userId);

            if (user != null)
            {
                if (password == user.Password)
                {
                    return true;
                }

                return false;
            }

            throw new ArgumentException("User does not exist!");
        }
    }
}