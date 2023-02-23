using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public class EFQuestionSubsection: IQuestionSubsection
    {
        public AppDbContext context;

        public IEnumerable<QuestionSubsection> QuestionSubsections => context.QuestionSubsection;

        public EFQuestionSubsection(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveQuestionSubsection(QuestionSubsection QuestionSubsection)
        {
            if (QuestionSubsection.QuestionSubsectionID == 0)
            {
                context.QuestionSubsection.Add(QuestionSubsection);
            }
            else
            {
                QuestionSubsection dbEntry = context.QuestionSubsection
                    .FirstOrDefault(c => c.QuestionSubsectionID == QuestionSubsection.QuestionSubsectionID);

                if (dbEntry != null)
                {
                    dbEntry.QuestionID = QuestionSubsection.QuestionID;      
                    dbEntry.SubsectionID = QuestionSubsection.SubsectionID;                                     
                }
            }

            context.SaveChanges(); //commit to db
        }

        public QuestionSubsection DeleteQuestionSubsection(int QuestionSubsectionID)
        {
            QuestionSubsection dbEntry = context.QuestionSubsection
                .FirstOrDefault(c => c.QuestionSubsectionID == QuestionSubsectionID);

            if (dbEntry != null)
            {
                context.QuestionSubsection.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
