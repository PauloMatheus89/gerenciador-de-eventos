using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class EventFavoriteDTO
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public int FavoriteId { get; set; }
    }
}