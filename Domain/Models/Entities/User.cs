using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GerenciadorEventos.Models
{
    public class User : IdentityUser
    {
        [Required]
        public Role Role { get; set; }


        //Navigation Properties
        [ValidateNever]
        public Organizer? Organizer { get; set; }
        [ValidateNever]
        public Participant? Participant { get; set; }
        [ValidateNever]
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
        [ValidateNever]
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        [ValidateNever]
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        //TO:DO - To String Method

        public override string ToString()
        {
            var organizerInfo = Organizer != null ? Organizer.ToString() : "null";
            
            return $"User {{ Id = {Id}, Username = {UserName}, Email = {Email}, Role = {Role}, " +
                $"Organizer = {organizerInfo}, " +
                $"Participant = {(Participant != null ? Participant.ToString() : "null")}, " +
                $"Inscriptions = {Inscriptions?.Count ?? 0}, " +
                $"Payments = {Payments?.Count ?? 0}, " +
                $"Favorites = {Favorites?.Count ?? 0} }}";
            }

    }
    
    
}