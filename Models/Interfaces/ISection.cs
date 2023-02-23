
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface ISection
    {
        IQueryable<Section> Sections { get; }

        Task SaveSection(Section Section);

        Section DeleteSection(int SectionID);
    }
}
