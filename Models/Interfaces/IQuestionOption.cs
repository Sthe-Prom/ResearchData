
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface IQuestionOption
    {
        IEnumerable<QuestionOption> QuestionOptions { get; }

        Task SaveQuestionOption(QuestionOption QuestionOption);

        QuestionOption DeleteQuestionOption(int QuestionOptionID);
    }
}
