using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.CustomValidations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class OrganizerDto
    {
        [Required]
        [Display(Name = "Corporate Email")]
        [EmailAddress(ErrorMessage = "This {0} is invalid")]
        public string? CorporateEmail { get; set; }

        [Required]
        [Display(Name = "Corporate Name")]
        [StringLength(100, ErrorMessage = "{0} is too long!")]
        public string? CorporateName { get; set; }

        [Required(ErrorMessage = "Document is Required")]
        [DocumentValidation]
        public string? Document { get; set; }
        public string? Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}