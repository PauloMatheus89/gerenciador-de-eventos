using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;

namespace GerenciadorEventos.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public CategoryName? Name { get; set; }
        [Required]
        public string? Description { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}