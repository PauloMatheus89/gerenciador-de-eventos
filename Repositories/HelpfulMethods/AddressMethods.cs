using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            else if (addressToUpdate == null && address !=null)
            {
                if (AddressExists(context, address))
                    context.Addresses.Attach(address);
            }

        }
        
    }
}