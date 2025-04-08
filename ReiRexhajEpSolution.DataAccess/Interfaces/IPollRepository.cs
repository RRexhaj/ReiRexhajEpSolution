using System.Collections.Generic;
using ReiRexhajEpSolution.Domain.Models;

namespace ReiRexhajEpSolution.DataAccess.Interfaces
{
    public interface IPollRepository
    {
        void CreatePoll(Poll poll);
        void CreatePoll(Poll poll, Microsoft.Extensions.Logging.ILogger logger);
        IEnumerable<Poll> GetPolls();
        Poll GetPollById(int id);
        void Vote(int pollId, int optionNumber);
        void UpdatePoll(Poll poll);
    }
}
