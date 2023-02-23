using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public class EFQuestion: IQuestion
    {
        public AppDbContext context;

        public IEnumerable<Question> Questions => context.Question;

        public EFQuestion(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveQuestion(Question Question)
        {
            if (Question.QuestionID == 0)
            {
                context.Question.Add(Question);
            }
            else
            {
                Question dbEntry = context.Question
                    .FirstOrDefault(c => c.QuestionID == Question.QuestionID);

                if (dbEntry != null)
                {
                    dbEntry.Question_ = Question.Question_;
                    dbEntry.QuestionAbbr = Question.QuestionAbbr;
                    dbEntry.QuestionTypeID = Question.QuestionTypeID;
                   
                }
            }

            context.SaveChanges(); //commit to db
        }

        public Question DeleteQuestion(int QuestionID)
        {
            Question dbEntry = context.Question
                .FirstOrDefault(c => c.QuestionID == QuestionID);

            if (dbEntry != null)
            {
                context.Question.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
