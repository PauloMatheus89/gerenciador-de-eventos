using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "City Name")]
        [StringLength(50, ErrorMessage = "{0} is too big!")]
        public string? CityName { get; set; }
        [Required]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "Invalid {0}")]
        public string? CEP { get; set; }
        [StringLength(100, ErrorMessage = "{0} is too big")]
        [Display(Name = "Street Name")]
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public int Number { get; set; }
        [StringLength(100, ErrorMessage = "{0} is too big")]
        public string? Neighborhood { get; set; }

        //Navigation Property
        public Organizer? Organizer { get; set; }
        public Event? Event { get; set; }
    }
}