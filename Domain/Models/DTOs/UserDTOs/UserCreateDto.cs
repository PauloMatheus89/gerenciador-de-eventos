using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} is too long!")]
        public string? Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]+$", ErrorMessage = "Invalid Password! It must contain at leat: 1 Letter and 1 Number")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long and maximum 10 characters")]
        public string? Password { get; set; }

        [Required]
        public Role Role { get; set; }
        [Required]
        public string? Email { get; set; }



    }
}