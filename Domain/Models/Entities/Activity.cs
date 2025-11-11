using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Interfaces.IEntities;

namespace GerenciadorEventos.Domain.Models.Entities
{
    //TODO: Validation for StartTime e Endtime 
    // (Verify if StartTime is after EndTime)
    // (Verify if it is an valid Date)
    public class Activity : IEntityWithDay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} is way too long!")]
        public string? Title { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        public string? Description { get; set; }

        public int DayId { get; set; }

        public Day Day { get; set; } = null!;

    }
}