using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories
{
    public class OrganizerRepository
    {
        private readonly DatabaseContext _context;

        public OrganizerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Create(Organizer Organizer)
        {
            _context.Organizers.Add(Organizer);
            _context.SaveChanges();
        }

        public void Delete(Organizer organizer)
        {
            _context.Organizers.Remove(organizer);
            _context.SaveChanges();
        }

        public Organizer? GetByid(int id)
        {
            var organizer = _context.Organizers
                            .Include(o => o.User)
                            .Include(o => o.Address)
                            .FirstOrDefault(o => o.Id == id);

            return organizer;
        }

        
        public void Update(int id, Organizer organizer)
        {
            //Não Inclui as propriedade de navegação
            //var updateOrganizer = _context.Organizers.Find(id);

            var updateOrganizer = _context.Organizers
                .Include(o => o.Address)
                .Include(o => o.Events)
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == id);

            if (updateOrganizer == null)
            {
                Console.WriteLine("Organizer was not Found!");
            }
            else
            {
                updateOrganizer.CorporateName = organizer.CorporateName;
                updateOrganizer.CorporateEmail = organizer.CorporateEmail;
                updateOrganizer.Description = organizer.Description;
                updateOrganizer.Document = organizer.Document;

                if (updateOrganizer.Address != null && organizer.Address != null)
                {
                    updateOrganizer.Address.CityName = organizer.Address.CityName;
                    updateOrganizer.Address.CEP = organizer.Address.CEP;
                    updateOrganizer.Address.StreetName = organizer.Address.StreetName;
                    updateOrganizer.Address.Number = organizer.Address.Number;
                    updateOrganizer.Address.Neighborhood = organizer.Address.Neighborhood;
                }
                else if (updateOrganizer.Address == null)
                {
                    if (organizer.Address != null && _context.Addresses.Any(e => e.Id == organizer.Address.Id))
                    {
                        _context.Addresses.Attach(organizer.Address);
                    }
                    updateOrganizer.Address = organizer.Address;
                }

                if (organizer.Events != updateOrganizer.Events)
                {
                    foreach (var evt in organizer.Events)
                    {
                        if (!updateOrganizer.Events.Any(e => e.Id == evt.Id))
                            updateOrganizer.Events.Add(evt);
                    }

                    var toDelete = updateOrganizer.Events.Where(e => !organizer.Events.Any(evt => evt.Id == e.Id)).ToList();
                    foreach(var eventToDelete in toDelete)
                    {
                        updateOrganizer.Events.Remove(eventToDelete);
                    }

                    
                }

                if(updateOrganizer.UserId != 0 && updateOrganizer.UserId != organizer.UserId)
                {
                    var newRegister = _context.Users.Find(organizer.UserId);
                    if(newRegister != null)
                    {
                        updateOrganizer.UserId = organizer.UserId;
                        updateOrganizer.User = newRegister;
                    }
                }
                //TODO: Fazer o mesmo para Event - se a lista de eventos estiver vazia adicionar
                //Caso contrario, procurar pelo Id do event antes de altera-lo
                //Verificar se viavel

                _context.SaveChanges();
            }


        }

        public List<Organizer> GetAllOrganizers()
        {
            return _context.Organizers
            .Include(o => o.Address)
            .Include(o => o.User)
            .ToList();
        }

    }
}