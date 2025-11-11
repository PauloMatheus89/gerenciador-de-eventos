using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.CustomValidations;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Title is to Big")]
        public string? Title { get; set; }
        [Required]
        [Range(0,float.MaxValue, ErrorMessage = "Value can't be less than 0")]
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

        [ForeignKey("Organizer")]
        public int OrganizerId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Organizer Organizer { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
        public ICollection<Day> Days { get; set; } = new List<Day>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}