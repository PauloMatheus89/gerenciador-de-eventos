using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;

namespace GerenciadorEventos.Domain.Models.DTOs
{
    public class InscriptionDTO
    {
        [Required]
        //TODO Validação para inscriptionDate
        public DateTime InscriptionDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PaymentId { get; set; }
    }
}