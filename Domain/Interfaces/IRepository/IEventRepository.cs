using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IEventRepository
    {
        void Create(Event @event);
        void Update(int id,Event @event);
        void Remove(Event @event);
        Event GetById(int id);
    }
}