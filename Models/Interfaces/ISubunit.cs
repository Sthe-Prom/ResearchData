
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public interface ISubunit
    {

        IEnumerable<Subunit> Subunits { get; }

        Task SaveSubunit(Subunit Subunit);

        Subunit DeleteSubunit(int SubunitID);
    }
}
