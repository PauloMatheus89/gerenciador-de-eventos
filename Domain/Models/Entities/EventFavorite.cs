using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Domain.Models.Entities
{
    public class EventFavorite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        [ForeignKey("Favorite")]
        public int FavoriteId { get; set; }
        public Event Event { get; set; } = null!;
        public Favorite Favorite { get; set; } = null!;
    }
}