using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class PaymentDto
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "{0} can't be less than 0!")]
        public double Value { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public Status Status { get; set; }
        public int UserId { get; set; }

    }
}