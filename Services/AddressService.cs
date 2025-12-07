using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Exceptions;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace GerenciadorEventos.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IOrganizerRepository _organizerRepository;
        //TODO: Adicionar IDayRepository

        public AddressService(IAddressRepository addressRepository,IOrganizerRepository organizerRepository)
        {
            _addressRepository = addressRepository;
            _organizerRepository = organizerRepository;
        }

        public async Task<Address> AddAddress(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException("Address Received is Null");
            }

            if (addressDto.CEP != null && await _addressRepository.CepExists(addressDto.CEP))
                throw new DuplicateNameException("Error in Create(Address) - This Cep is already being used by other address!");


            var newAddress = new Address
            {
                CEP = addressDto.CEP,
                CityName = addressDto.CityName,
                State = addressDto.State,
                StreetName = addressDto.StreetName,
                Number = addressDto.Number,
                Neighborhood = addressDto.Neighborhood,
                OrganizerId = addressDto.OrganizerId!.Value, //null forcing
                DayId = addressDto.DayId!.Value

            };

            var address = await _addressRepository.Create(newAddress);

            return address;
            
        }

        public async Task<Address?> GetById(int addressId)
        {
            return await _addressRepository.GetById(addressId);
        } 

        public async Task<Address> RemoveAddress(int id)
        {
            var addressToDelete = await _addressRepository.GetById(id);

            if (addressToDelete == null)
                throw new KeyNotFoundException("Error in Demove(Address) - Could not find a Key to delete");

            if (await _organizerRepository.GetByid(addressToDelete.OrganizerId) != null)
                throw new ActiveOrganizerException();

            var address = await _addressRepository.Remove(addressToDelete);

            return address;
        }

        public async Task<Address> UpdateAddress(int id, AddressDto addressDto)
        {
            var addressToUpdate = await _addressRepository.GetById(id);

            if (addressDto == null)
            {
                throw new ArgumentNullException("Error in Update(Address) - Address received is null");
            }

            if (addressToUpdate == null)
                throw new KeyNotFoundException("Could not find an Address related to that Id");

            if (addressDto.CEP != addressToUpdate.CEP && await _addressRepository.CepExists(addressDto.CEP!))
            {
                throw new DuplicateNameException($"Error in Update(Address) - The Cep {addressDto.CEP} is already being used by other address!");
            }

            var newAddress = new Address
            {
                CityName = addressDto.CityName,
                State = addressDto.State,
                Number = addressDto.Number,
                Neighborhood = addressDto.Neighborhood,
                StreetName = addressDto.StreetName,
                CEP = addressDto.CEP
            };

            var address = await _addressRepository.Update(addressToUpdate, newAddress);

            return address;
        }

        //TODO Metodo para atualizar OrganizerId
        
        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await _addressRepository.GetAllAddresses();
        }


        
        private string NormalizeString(string? word)
        {
            return string.IsNullOrWhiteSpace(word) ? string.Empty : word.Trim().ToLower().Replace(" ", "");
        }

    }
}