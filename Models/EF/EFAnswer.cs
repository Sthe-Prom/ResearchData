using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ResearchData.Models
{
    public class EFAnswer: IAnswer
    {
        public AppDbContext context;

        public IQueryable<Answer> Answers => context.Answer;

        public EFAnswer(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveAnswer(Answer Answer)
        {
            if (Answer.AnswerID == 0)
            {
                context.Answer.Add(Answer);
            }
            else
            {
                Answer dbEntry = context.Answer
                    .FirstOrDefault(c => c.AnswerID == Answer.AnswerID);

                if (dbEntry != null)
                {
                    dbEntry.Answer_ = Answer.Answer_;
                    dbEntry.SheetID = Answer.SheetID;
                    dbEntry.QuestionID = Answer.QuestionID;
                    dbEntry.OptionID = Answer.OptionID;
                   
                }
            }

            context.SaveChanges(); //commit to db
        }

        public Answer DeleteAnswer(int AnswerID)
        {
            Answer dbEntry = context.Answer
                .FirstOrDefault(c => c.AnswerID == AnswerID);

            if (dbEntry != null)
            {
                context.Answer.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
