using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class CategoryDto
    {
        [Required]
        public CategoryName Name { get; set; }

        [Required]
        public string? Description { get; set; }

    }
}