using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace GerenciadorEventos.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public void AddAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("Address Received is Null");
            }

            _addressRepository.Create(address);
            
        }

        public Address? GetById(int addressId)
        {
            throw new NotImplementedException();
        }

        public void RemoveAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("Address received is null");
            }

            _addressRepository.Remove(address);
        }

        public void UpdateAddress(int id, Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("Address received is null");
            }

            if (verifyDuplicatedAddress(address))
            {
                throw new ArgumentException("This Address Register already exists");
            }

            _addressRepository.Update(id, address);
        }

        private bool verifyDuplicatedAddress(Address address)
        {
            var allAddresses = _addressRepository.GetAllAddresses();

            var normalizedCityName = NormalizeString(address.CityName!);
            var normalizedCEP = NormalizeString(address.CEP!);
            var normalizedStreetName = NormalizeString(address.StreetName!);
            var normalizedNeighborhood = NormalizeString(address.Neighborhood!);

            if (allAddresses.Any(a =>
                NormalizeString(a.CityName) == normalizedCityName &&
                NormalizeString(a.CEP) == normalizedCEP &&
                NormalizeString(a.StreetName) == normalizedStreetName &&
                NormalizeString(a.Neighborhood) == normalizedNeighborhood &&
                a.Number == address.Number
                ))
            {
                return true;
            }

            return false;
        }
        
        private string NormalizeString(string? word)
        {
            return string.IsNullOrWhiteSpace(word) ? string.Empty : word.Trim().ToLower().Replace(" ", "");
        }

    }
}