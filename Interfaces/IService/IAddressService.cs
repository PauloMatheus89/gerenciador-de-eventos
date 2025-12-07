using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IAddressService
    {
        Task<Address> AddAddress(AddressDto addressDto);
        Task<Address> RemoveAddress(int id);
        Task<Address> UpdateAddress(int id,AddressDto addressDto);
        Task<Address?> GetById(int addressId);
        Task<IEnumerable<Address>> GetAddresses();

    }
}