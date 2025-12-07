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
    public class Favorite : IEntityWithUser<int?>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        //Navigation Properties
        public Event Event { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<EventFavorite> EventFavorites { get; set; } = new List<EventFavorite>();
    }
}