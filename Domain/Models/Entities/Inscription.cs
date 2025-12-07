using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;
using GerenciadorEventos.Interfaces.IEntities;

namespace GerenciadorEventos.Models
{
    public class Inscription : IEntityWithEvent , IEntityWithUser<int>, IEntityWithPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime InscriptionDate { get; set; }
        public Status Status { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Payment")]
        public int PaymentId { get; set; }

        //Navigation Properties
        public User User { get; set; } = null!;
        public Event Event { get; set; } = null!;
        public Payment Payment { get; set; } = null!;
        
    }
}