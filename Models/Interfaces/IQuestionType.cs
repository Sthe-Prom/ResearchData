
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface IQuestionType
    {
        IEnumerable<QuestionType> QuestionTypes { get; }

        Task SaveQuestionType(QuestionType QuestionType);

        QuestionType DeleteQuestionType(int QuestionTypeID);
    }
}
