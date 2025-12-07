using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class ActivityDTO
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }
        public string? Description { get; set; }
        
        [Required]
        public int DayId { get; set; }

    }
}