using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DatabaseContext _context;

        public AddressRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Create(Address address)
        {
            _context.Add(address);
            _context.SaveChanges();
        }

        public void Remove(Address address)
        {
            _context.Remove(address);
            _context.SaveChanges();
        }

        public void Update(int id, Address address)
        {
            var updateAddress = _context.Addresses
                            .Include(a => a.Organizer)
                            .Include(a => a.Event)
                            .FirstOrDefault(a => a.Id == id);

            if (address.Organizer != null && updateAddress!.Organizer != address.Organizer)
                updateAddress.Organizer = address.Organizer;

            if (address.Event != null && updateAddress!.Event != address.Event)
                updateAddress.Event = address.Event;

            updateAddress!.StreetName = address.StreetName;
            updateAddress.CityName = address.CityName;
            updateAddress.CEP = address.CEP;
            updateAddress.Neighborhood = address.Neighborhood;
            updateAddress.Number = address.Number;

            _context.SaveChanges();
        }

        public Address? GetById(int id)
        {
            return _context.Addresses
                    .Include(a => a.Organizer)
                    .Include(a => a.Event)
                    .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return _context.Addresses
                    .Include(a => a.Organizer)
                    .Include(a => a.Event)
                    .ToList();
        }
    }
}