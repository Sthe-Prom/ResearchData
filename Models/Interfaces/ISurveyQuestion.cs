
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public interface ISurveyQuestion
    {
        IEnumerable<SurveyQuestion> SurveyQuestions { get; }

        Task SaveSurveyQuestion(SurveyQuestion SurveyQuestion);

        SurveyQuestion DeleteSurveyQuestion(int SurveyQuestionID);
    }
}
