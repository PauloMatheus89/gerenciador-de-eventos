using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;

namespace GerenciadorEventos.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "{0} can't be less than 0!")]
        public double Value { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public Status Status { get; set; }

        //Navigation Properties
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();

    }
}