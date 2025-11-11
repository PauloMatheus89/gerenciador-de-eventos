using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Interfaces.IEntities;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Domain.Models.Entities
{
    //TO:DO: Validation For Date, OpeningTime, ClosingTime
    public class Day : IEntityWithEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime @Date { get; set; }

        [Required]
        public TimeSpan OpeningTime { get; set; }

        [Required]
        public TimeSpan ClosingTime { get; set; }

        public string? Description { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        //Navigation Properties
        public Event Event { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

        
    }
}