using Microsoft.AspNetCore.Mvc;
using ReiRexhajEpSolution.DataAccess.Interfaces;
using ReiRexhajEpSolution.Domain.Models;
using ReiRexhajEpSolution.Presentation.Filters;

namespace ReiRexhajEpSolution.Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls();
            return View(polls);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
            {
                return NotFound();
            }
            return View(poll);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Poll poll)
        {
            if (ModelState.IsValid)
            {
                poll.DateCreated = DateTime.Now;
                _pollRepository.CreatePoll(poll);
                return RedirectToAction("Index");
            }
            return View(poll);
        }

        [HttpPost]
        [ServiceFilter(typeof(OneVotePerPollFilter))]
        public IActionResult Vote(int pollId, int option)
        {
            _pollRepository.Vote(pollId, option);
            return RedirectToAction("Details", new { id = pollId });
        }
    }
}
