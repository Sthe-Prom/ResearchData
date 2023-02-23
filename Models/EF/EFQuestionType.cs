using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public class EFQuestionType: IQuestionType
    {
        public AppDbContext context;

        public IEnumerable<QuestionType> QuestionTypes => context.QuestionType;

        public EFQuestionType(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveQuestionType(QuestionType QuestionType)
        {
            if (QuestionType.QuestionTypeID == 0)
            {
                context.QuestionType.Add(QuestionType);
            }
            else
            {
                QuestionType dbEntry = context.QuestionType
                    .FirstOrDefault(c => c.QuestionTypeID == QuestionType.QuestionTypeID);

                if (dbEntry != null)
                {
                    dbEntry.QuestionTypeName = QuestionType.QuestionTypeName;                                     
                }
            }

            context.SaveChanges(); //commit to db
        }

        public QuestionType DeleteQuestionType(int QuestionTypeID)
        {
            QuestionType dbEntry = context.QuestionType
                .FirstOrDefault(c => c.QuestionTypeID == QuestionTypeID);

            if (dbEntry != null)
            {
                context.QuestionType.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
