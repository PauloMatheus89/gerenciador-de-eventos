using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IEntities
{
    public interface IEntityWithUser
    {
        int UserId { get; set; }
        User User { get; set; }
    }
}