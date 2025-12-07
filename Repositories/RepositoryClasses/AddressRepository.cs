using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using GerenciadorEventos.Repositories.HelpfulMethods;
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

        public async Task<Address> Create(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<Address> Remove(Address address)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }


        public async Task<Address> Update(Address addressToUpdate, Address newAddress)
        {
            addressToUpdate.CityName = newAddress.CityName;
            addressToUpdate.CEP = newAddress.CEP;
            addressToUpdate.Neighborhood = newAddress.Neighborhood;
            addressToUpdate.Number = newAddress.Number;
            addressToUpdate.State = newAddress.State;
            addressToUpdate.StreetName = newAddress.StreetName;

            await _context.SaveChangesAsync();

            return addressToUpdate;
        }

        public async Task<Address?> GetById(int id)
        {
            return await _context.Addresses
                    .Include(a => a.Organizer)
                    .Include(a => a.Day)
                    .FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<Address?> GetByCep(string cep)
        {
            return _context.Addresses
                    .Include(a => a.Organizer)
                    .Include(a => a.Day)
                    .FirstOrDefaultAsync(a => a.CEP == cep);
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            return await _context.Addresses
                    .Include(a => a.Organizer)
                    .Include(a => a.Day)
                    .ToListAsync();
        }

        public async Task<bool> CepExists(string cep)
        {
            return await _context.Addresses.AnyAsync(e => e.CEP == cep);
        }
    }
}