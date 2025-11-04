using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IAddressRepository
    {
        void Create(Address address);
        void Update(int id, Address address);
        void Remove(Address address);
        Address GetById(int id);
        
    }
}