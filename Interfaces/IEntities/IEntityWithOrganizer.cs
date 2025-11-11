using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IEntities
{
    public interface IEntityWithOrganizer
    {
        int? OrganizerId { get; set; }
        Organizer? Organizer { get; set; }
    }
}