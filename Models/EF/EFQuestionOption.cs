using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{

    public class EFQuestionOption: IQuestionOption
    {
        public AppDbContext context;

        public IEnumerable<QuestionOption> QuestionOptions => context.QuestionOption;

        public EFQuestionOption(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveQuestionOption(QuestionOption QuestionOption)
        {
            if (QuestionOption.QuestionOptionID == 0)
            {
                context.QuestionOption.Add(QuestionOption);
            }
            else
            {
                QuestionOption dbEntry = context.QuestionOption
                    .FirstOrDefault(c => c.QuestionOptionID == QuestionOption.QuestionOptionID);

                if (dbEntry != null)
                {
                    dbEntry.Option = QuestionOption.Option;  
                    dbEntry.QuestionID = QuestionOption.QuestionID;                                     
                }
            }

            context.SaveChanges(); //commit to db
        }

        public QuestionOption DeleteQuestionOption(int QuestionOptionID)
        {
            QuestionOption dbEntry = context.QuestionOption
                .FirstOrDefault(c => c.QuestionOptionID == QuestionOptionID);

            if (dbEntry != null)
            {
                context.QuestionOption.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
