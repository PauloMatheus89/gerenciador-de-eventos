using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Interfaces.IEntities;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Domain.Models.Entities
{
    public class Participant : IEntityWithUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name is too long")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "{0} providede is not an valid Email")]
        public string? Email { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}