using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class DayDTO
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan OpeningTime { get; set; }

        [Required]
        public TimeSpan ClosingTime { get; set; }

        public string? Description { get; set; }

        [Required]
        public int EventId { get; set; }
    }
}