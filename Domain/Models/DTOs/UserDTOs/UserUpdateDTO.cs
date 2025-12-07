using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;

namespace GerenciadorEventos.Domain.Models.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "This is not a valid email address.")]
        public string? Email { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}