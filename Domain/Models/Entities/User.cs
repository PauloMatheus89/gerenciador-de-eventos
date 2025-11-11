using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GerenciadorEventos.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
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
        [EmailAddress(ErrorMessage = "{0} is invalid!")]
        public string? Email { get; set; }
        
        //Navigation Properties
        
        public Organizer? Organizer { get; set; } 
        public Participant? Participant { get; set; }
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Favorite> Favorites { get; set; } = null!;
        //TO:DO - To String Method
    }
}