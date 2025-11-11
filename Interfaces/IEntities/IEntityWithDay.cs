using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IEntities
{
    public interface IEntityWithDay
    {
        int DayId { get; set; }

        Day Day { get; set; }
    }
}