
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface IAddress
    {

        IQueryable<Address> Addresses { get; }

        Task SaveAddress(Address Address);

        Address DeleteAddress(int AddressID);
    }
}
