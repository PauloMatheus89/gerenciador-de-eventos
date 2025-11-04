using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IAddressService
    {
        void AddAddress(Address address);
        void RemoveAddress(Address address);
        void UpdateAddress(int id,Address address);
        Address? GetById(int addressId);
    }
}