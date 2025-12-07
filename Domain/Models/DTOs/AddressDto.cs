using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Domain.Models.Enums;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class AddressDto : IValidatableObject
    {
        [Required]
        [Display(Name = "City Name")]
        [StringLength(50, ErrorMessage = "{0} is too big!")]
        public string? CityName { get; set; }

        [Required]
        [Display(Name = "Street Name")]
        public string? StreetName { get; set; }
        [Required]
        public State State { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "Invalid {0}")]
        public string? CEP { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} is too big")]
        public string? Neighborhood { get; set; }

        public int? OrganizerId { get; set; }

        public int? DayId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(OrganizerId == null && DayId == null)
            {
                yield return new ValidationResult(
                    "You must provide either OrganizerId or DayId.",
                    new[] {nameof(OrganizerId),nameof(DayId)}
                );
            }
        }
    }
}