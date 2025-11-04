using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IOrganizerRepository
    {
        void Create(Organizer organizer);
        void Delete(Organizer organizer);
        void Update(int id, Organizer organizer);
        Organizer GetByid(int id);

    }
}