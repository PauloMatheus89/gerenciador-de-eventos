using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Domain.Models.Enums;
using GerenciadorEventos.Interfaces.IEntities;

namespace GerenciadorEventos.Models
{
    public class Address : IEntityWithDay , IEntityWithOrganizer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "City Name")]
        [StringLength(50, ErrorMessage = "{0} is too big!")]
        public string? CityName { get; set; }

        [Required]
        public State State { get; set; }
        
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

        [ForeignKey("Day")]
        public int DayId { get; set; }

        [ForeignKey("Organizer")]
        public int? OrganizerId{ get; set; }

        //Navigation Property
        public Organizer? Organizer { get; set; } = null!;
        public Day Day { get; set; } = null!;
        
    }
}