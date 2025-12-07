using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class AddressMethods
    {
        public static bool AddressExists(DatabaseContext context, Address? address)
        {
            if (address != null && context.Addresses.Any(a => a.Id == address.Id))
                return true;

            return false;
        }
        public static bool AddressExists(DatabaseContext context, AddressDto? addressDto)
        {
            if (addressDto != null && context.Addresses.Any(a => a.CEP == addressDto.CEP))
                return true;

            return false;
        }

        public static void UpdateAddress(DatabaseContext context, Address? address, Address? addressToUpdate)
        {
            if (address != null && addressToUpdate != null)
            {
                addressToUpdate.Neighborhood = address.Neighborhood;
                addressToUpdate.CEP = address.CEP;
                addressToUpdate.Number = address.Number;
                addressToUpdate.CityName = address.CityName;
                addressToUpdate.State = address.State;
                addressToUpdate.StreetName = address.StreetName;
                DayMethods.UpdateDayId(context, addressToUpdate, address);
                OrganizerMethods.UpdateOrganizerId(context, addressToUpdate, address);
            }
            else if (addressToUpdate == null && address != null)
            {
                if (AddressExists(context, address))
                    context.Addresses.Attach(address);
            }

        }

        public static void UpdateAddressBase(DatabaseContext context, AddressDto? addressDto, Address? addressToUpdate)
        {
            if (addressDto != null && addressToUpdate != null)
            {
                addressToUpdate.Neighborhood = addressDto.Neighborhood;
                addressToUpdate.CEP = addressDto.CEP;
                addressToUpdate.Number = addressDto.Number;
                addressToUpdate.CityName = addressDto.CityName;
                addressToUpdate.State = addressDto.State;
                addressToUpdate.StreetName = addressDto.StreetName;
            }
            else if (addressToUpdate == null && addressDto != null)
            {
                if (AddressExists(context, addressDto))
                {
                    var address = context.Addresses.FirstOrDefault(a => a.CEP == addressDto.CEP);
                    context.Addresses.Attach(address!);
                }
            }

        }

        public static Address CreateAddress(DatabaseContext context, AddressDto addressDto)
        {
            if (addressDto == null)
                throw new ArgumentNullException("Cannot create an Address with a Null Value");

            var address = new Address
            {
                CityName = addressDto.CityName,
                StreetName = addressDto.StreetName,
                CEP = addressDto.CEP,
                Neighborhood = addressDto.Neighborhood,
                Number = addressDto.Number,
                State = addressDto.State
            };

            context.Addresses.Add(address);

            return address;
        }
        
    }
}