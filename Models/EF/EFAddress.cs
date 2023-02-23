using ResearchData.Models.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.EF
{
    public class EFAddress: IAddress
    {
        public AppDbContext context;

        public IQueryable<Address> Addresses => context.Address;

        public EFAddress(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveAddress(Address Address)
        {
            if (Address.AddressID == 0)
            {
                context.Address.Add(Address);
            }
            else
            {
                Address dbEntry = context.Address
                    .FirstOrDefault(c => c.AddressID == Address.AddressID);

                if (dbEntry != null)
                {
                    dbEntry.StreetNr = Address.StreetNr;
                    dbEntry.StreetName = Address.StreetName;
                    dbEntry.City = Address.City;
                    dbEntry.ZipCode = Address.ZipCode;                   
                    dbEntry.SubunitID = Address.SubunitID;
                }
            }

            context.SaveChanges(); //commit to db
        }

        public Address DeleteAddress(int AddressID)
        {
            Address dbEntry = context.Address
                .FirstOrDefault(c => c.AddressID == AddressID);

            if (dbEntry != null)
            {
                context.Address.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
