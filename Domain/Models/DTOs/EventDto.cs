using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.CustomValidations;
using GerenciadorEventos.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class EventDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title is to Big")]
        public string? Title { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Value can't be less than 0")]
        public float EntryFee { get; set; }

        public string? Description { get; set; }

        [Required]
        [DateRangeValidation("EndDate")]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Total number of vacancies")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} cannot be less than 0")]
        public int TotalVacancies { get; set; }

        [Required]
        [Display(Name = "The number of vacancies")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} cannot be less than 0")]
        [VacanciesAvaiableValidation("TotalVacancies")]
        public int AvaiableVacancies { get; set; }

        [Required]
        public int OrganizerId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        


    }
}