using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using ReiRexhajEpSolution.Domain.Models;
using ReiRexhajEpSolution.DataAccess.Context;
using ReiRexhajEpSolution.DataAccess.Interfaces;

namespace ReiRexhajEpSolution.DataAccess.Repositories
{
    public class PollRepositoryMain : IPollRepository
    {
        private readonly PollDbContext _context;
        private readonly ILogger<PollRepositoryMain> _logger;

        public PollRepositoryMain(PollDbContext context, ILogger<PollRepositoryMain> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void CreatePoll(Poll poll)
        {
            poll.DateCreated = DateTime.Now;
            _context.Polls.Add(poll);
            _context.SaveChanges();
        }

        public void CreatePoll(Poll poll, ILogger logger)
        {
            logger.LogInformation("Creating poll: " + poll.Title);
            CreatePoll(poll);
        }

        public IEnumerable<Poll> GetPolls()
        {
            return _context.Polls.OrderByDescending(p => p.DateCreated).ToList();
        }

        public Poll GetPollById(int id)
        {
            return _context.Polls.FirstOrDefault(p => p.Id == id);
        }

        public void Vote(int pollId, int optionNumber)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == pollId);
            if (poll != null)
            {
                switch (optionNumber)
                {
                    case 1:
                        poll.Option1VotesCount++;
                        break;
                    case 2:
                        poll.Option2VotesCount++;
                        break;
                    case 3:
                        poll.Option3VotesCount++;
                        break;
                }
                _context.SaveChanges();
            }
        }

        public void UpdatePoll(Poll updatedPoll)
        {
            var existingPoll = _context.Polls.FirstOrDefault(p => p.Id == updatedPoll.Id);
            if (existingPoll != null)
            {
                existingPoll.Title = updatedPoll.Title;
                existingPoll.Option1Text = updatedPoll.Option1Text;
                existingPoll.Option2Text = updatedPoll.Option2Text;
                existingPoll.Option3Text = updatedPoll.Option3Text;
                _context.SaveChanges();
            }
        }
    }
}
