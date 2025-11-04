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
            var organizer = _context.Organizers.Find();

            return organizer;
        }

        
        public void Update(int id, Organizer organizer)
        {
            //Não Inclui as propriedade de navegação
            //var updateOrganizer = _context.Organizers.Find(id);

            var updateOrganizer = _context.Organizers
                .Include(o => o.Address)
                .Include(o => o.Event)
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

                if (organizer.AddressId != updateOrganizer.AddressId)
                    updateOrganizer.AddressId = organizer.AddressId;

                if (updateOrganizer.Address != null && organizer.Address != null)
                {
                    updateOrganizer.Address.CityName = organizer.Address.CityName;
                    updateOrganizer.Address.CEP = organizer.Address.CEP;
                    updateOrganizer.Address.StreetName = organizer.Address.StreetName;
                    updateOrganizer.Address.Number = organizer.Address.Number;
                    updateOrganizer.Address.Neighborhood = organizer.Address.Neighborhood;
                }
                else if (updateOrganizer.Address == null && organizer.Address != null)
                {
                    updateOrganizer.Address = organizer.Address;
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