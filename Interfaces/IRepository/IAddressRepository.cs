using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IAddressRepository
    {
        Task<Address> Create(Address address);
        Task<Address> Update(Address addressToUpdate, Address newAddress);
        Task<Address> Remove(Address address);
        Task<Address?> GetById(int id);
        Task<Address?> GetByCep(string cep);
        Task<IEnumerable<Address>> GetAllAddresses();
        Task<bool> CepExists(string cep);
        
    }
}