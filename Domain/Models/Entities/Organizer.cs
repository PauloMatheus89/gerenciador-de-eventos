using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.CustomValidations;

namespace GerenciadorEventos.Models
{
    public class Organizer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Corporate Name")]
        [StringLength(100, ErrorMessage = "{0} is too long!")]
        public string? CorporateName { get; set; }
        //It may be CPF or CNPJ
        [Required(ErrorMessage = "Document is Required")]
        [DocumentValidation]
        public string? Document { get; set; }
        [Required]
        [Display(Name = "Corporate Email")]
        [EmailAddress(ErrorMessage = "This {0} is invalid")]
        public string? CorporateEmail { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }

        //Navigation Property
        public User? User { get; set; } 
        public Address? Address { get; set; } 
        public ICollection<Event> Event { get; set; } = new List<Event>();
    }
}