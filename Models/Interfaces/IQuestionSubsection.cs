
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface IQuestionSubsection
    {
        IEnumerable<QuestionSubsection> QuestionSubsections { get; }

        Task SaveQuestionSubsection(QuestionSubsection QuestionSubsection);

        QuestionSubsection DeleteQuestionSubsection(int QuestionSubsectionID);
    }
}
