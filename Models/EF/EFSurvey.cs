using ResearchData.Models.EF;
using ResearchData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public class EFSurvey: ISurvey
    {
        public AppDbContext context;

        public IEnumerable<Survey> Surveys => context.Survey;

        public EFSurvey(AppDbContext ctx)
        {
            context = ctx;
        }

        public async Task SaveSurvey(Survey Survey)
        {
            if (Survey.SurveyID == 0)
            {
                context.Survey.Add(Survey);
            }
            else
            {
                Survey dbEntry = context.Survey
                    .FirstOrDefault(c => c.SurveyID == Survey.SurveyID);

                if (dbEntry != null)
                {
                    dbEntry.SurveyName = Survey.SurveyName;
                    dbEntry.SurveyDate = Survey.SurveyDate;
                    dbEntry.AccountID = Survey.AccountID;                                     
                }

               
            }

            context.SaveChanges(); //commit to db
        }

        public Survey DeleteSurvey(int SurveyID)
        {
            Survey dbEntry = context.Survey
                .FirstOrDefault(c => c.SurveyID == SurveyID);

            if (dbEntry != null)
            {
                context.Survey.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

    }
}
