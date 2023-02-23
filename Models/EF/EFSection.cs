using ResearchData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResearchData.Models.Interfaces;
using ResearchData.Models.EF;

namespace ResearchData.Models
{
public class EFSection: ISection
    {
        public AppDbContext context;

        public IQueryable<Section> Sections => context.Section;

        public EFSection(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveSection(Section Section)
        {
            if (Section.SectionID == 0)
            {
                context.Section.Add(Section);
            }
            else
            {
                Section dbEntry = context.Section
                    .FirstOrDefault(c => c.SectionID == Section.SectionID);

                if (dbEntry != null)
                {
                    dbEntry.SectionName = Section.SectionName;                                     
                }
            }

            context.SaveChanges(); //commit to db
        }

        public Section DeleteSection(int SectionID)
        {
            Section dbEntry = context.Section
                .FirstOrDefault(c => c.SectionID == SectionID);

            if (dbEntry != null)
            {
                context.Section.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
