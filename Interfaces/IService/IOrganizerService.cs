using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IOrganizerService
    {
        void AddOrganizer(Organizer organizer);
        void RemoveOrganizer(Organizer organizer);
        void UpdateOrganizer(int id,Organizer organizer);
        Organizer? GetById(int organizerId);
    }
}