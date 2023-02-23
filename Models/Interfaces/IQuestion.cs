
using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface IQuestion
    {
        IEnumerable<Question> Questions { get; }

        Task SaveQuestion(Question Question);

        Question DeleteQuestion(int QuestionID);
    }
}
