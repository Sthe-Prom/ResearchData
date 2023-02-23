
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface ISubsection
    {
        IEnumerable<Subsection> Subsections { get; }

        Task SaveSubsection(Subsection Subsection);

        Subsection DeleteSubsection(int SubsectionID);
    }
}
