using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IEntities
{
    public interface IEntityWithEvent
    {
        int EventId { get; set; }
        Event Event { get; set; }
    }
}