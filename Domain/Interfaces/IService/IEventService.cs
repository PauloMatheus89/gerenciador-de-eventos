using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IEventService
    {
        void AddEvent(Event var);
        void RemoveEvent(Event var);
        void UpdateEvent(int id,Event var);
        Event? GetById(int eventId);
    }
}