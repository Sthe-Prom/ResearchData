using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public class EFSubunit: ISubunit
    {
        public AppDbContext context;

        public IEnumerable<Subunit> Subunits => context.Subunit;

        public EFSubunit(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveSubunit(Subunit Subunit)
        {
            if (Subunit.SubunitID == 0)
            {
                context.Subunit.Add(Subunit);
            }
            else
            {
                Subunit dbEntry = context.Subunit
                    .FirstOrDefault(c => c.SubunitID == Subunit.SubunitID);

                if (dbEntry != null)
                {
                    dbEntry.SubunitName = Subunit.SubunitName;
                    dbEntry.SubunitDesc = Subunit.SubunitDesc;
                    dbEntry.SubunitLogo = Subunit.SubunitLogo;
                    dbEntry.SubunitEmail = Subunit.SubunitEmail;                   
                    dbEntry.SubunitCell = Subunit.SubunitCell;
                    dbEntry.SubunitServerAddress = Subunit.SubunitServerAddress;

                }
            }

            context.SaveChanges(); //commit to db
        }

        public Subunit DeleteSubunit(int SubunitID)
        {
            Subunit dbEntry = context.Subunit
                .FirstOrDefault(c => c.SubunitID == SubunitID);

            if (dbEntry != null)
            {
                context.Subunit.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
