using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Domain.Models.DTOs.UserDTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "This is not a valid email address")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]+$", ErrorMessage = "Invalid Password! It must contain at leat: 1 Letter and 1 Number")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long and maximum 10 characters")]
        public string? Password { get; set; }
    }
}