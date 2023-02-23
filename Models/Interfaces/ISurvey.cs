
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models
{
    public interface ISurvey
    {
        IEnumerable<Survey> Surveys { get; }

        Task SaveSurvey(Survey Survey);

        Survey DeleteSurvey(int SurveyID);
    }
}
