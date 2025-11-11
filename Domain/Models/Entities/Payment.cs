using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;
using GerenciadorEventos.Interfaces.IEntities;

namespace GerenciadorEventos.Models
{
    public class Payment : IEntityWithUser
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
        [Required]
        public Status Status { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        //Navigation Properties
        public User User { get; set; } = null!;
        public Inscription Inscription { get; set; } = null!;

    }
}