using System.Collections.Generic;
using ReiRexhajEpSolution.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ReiRexhajEpSolution.DataAccess.Interfaces
{
    public interface IPollRepository
    {
        void CreatePoll(Poll poll);
        // Overload for method injection demonstration.
        void CreatePoll(Poll poll, ILogger logger);
        IEnumerable<Poll> GetPolls();
        Poll GetPollById(int id);
        void Vote(int pollId, int optionNumber);
    }
}
