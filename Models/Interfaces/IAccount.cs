using ResearchData.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchData.Models.Interfaces
{
    public interface IAccount
    {

        IEnumerable<Account> Accounts { get; }

        Task SaveAccount(Account Account);

        Account DeleteAccount(int AccountID);
    }
}
