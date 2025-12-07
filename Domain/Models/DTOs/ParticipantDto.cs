using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class ParticipantDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "{0} provided is not an valid Email")]
        public string? Email { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}