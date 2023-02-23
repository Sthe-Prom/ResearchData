
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface IAnswer
    {
        IQueryable<Answer> Answers { get; }

        Task SaveAnswer(Answer Answer);

        Answer DeleteAnswer(int AnswerID);
    }
}
