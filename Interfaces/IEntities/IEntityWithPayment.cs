using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IEntities
{
    public interface IEntityWithPayment
    {
        int PaymentId { get; set; }
        Payment Payment { get; set; }
    }
}