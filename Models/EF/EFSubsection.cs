using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public class EFSubsection: ISubsection
    {
        public AppDbContext context;

        public IEnumerable<Subsection> Subsections => context.Subsection;

        public EFSubsection(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveSubsection(Subsection Subsection)
        {
            if (Subsection.SubsectionID == 0)
            {
                context.Subsection.Add(Subsection);
            }
            else
            {
                Subsection dbEntry = context.Subsection
                    .FirstOrDefault(c => c.SubsectionID == Subsection.SubsectionID);

                if (dbEntry != null)
                {
                    dbEntry.SubsectionName = Subsection.SubsectionName;      
                    dbEntry.SectionID = Subsection.SectionID;                                     
                }
            }

            context.SaveChanges(); //commit to db
        }

        public Subsection DeleteSubsection(int SubsectionID)
        {
            Subsection dbEntry = context.Subsection
                .FirstOrDefault(c => c.SubsectionID == SubsectionID);

            if (dbEntry != null)
            {
                context.Subsection.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
