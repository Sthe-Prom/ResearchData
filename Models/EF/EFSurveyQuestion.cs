using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public class EFSurveyQuestion: ISurveyQuestion
    {
        public AppDbContext context;

        public IEnumerable<SurveyQuestion> SurveyQuestions => context.SurveyQuestion;

        public EFSurveyQuestion(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveSurveyQuestion(SurveyQuestion SurveyQuestion)
        {
            if (SurveyQuestion.SurveyQuestionID == 0)
            {
                context.SurveyQuestion.Add(SurveyQuestion);
            }
            else
            {
                SurveyQuestion dbEntry = context.SurveyQuestion
                    .FirstOrDefault(c => c.SurveyQuestionID == SurveyQuestion.SurveyQuestionID);

                if (dbEntry != null)
                {
                    dbEntry.SurveyID = SurveyQuestion.SurveyID;      
                    dbEntry.QuestionID = SurveyQuestion.QuestionID;                                     
                }
            }

            context.SaveChanges(); //commit to db
        }

        public SurveyQuestion DeleteSurveyQuestion(int SurveyQuestionID)
        {
            SurveyQuestion dbEntry = context.SurveyQuestion
                .FirstOrDefault(c => c.SurveyQuestionID == SurveyQuestionID);

            if (dbEntry != null)
            {
                context.SurveyQuestion.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
