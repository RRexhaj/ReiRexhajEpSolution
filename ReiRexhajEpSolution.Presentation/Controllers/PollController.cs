using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiRexhajEpSolution.DataAccess.Interfaces;
using ReiRexhajEpSolution.Domain.Models;
using System;

namespace ReiRexhajEpSolution.Presentation.Controllers
{
    [Authorize]
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        // GET: /Poll/Index
        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls();
            return View(polls);
        }

        // GET: /Poll/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Poll/Create
        [HttpPost]
        public IActionResult Create(Poll poll)
        {
            if (ModelState.IsValid)
            {
                // Set the poll creation date
                poll.DateCreated = DateTime.Now;

                // Save the new poll via repository
                _pollRepository.CreatePoll(poll);

                return RedirectToAction("Index");
            }
            // If validation fails, reload form with errors
            return View(poll);
        }

        // GET: /Poll/Details/5
        public IActionResult Details(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null) return NotFound();
            return View(poll);
        }

        // POST: /Poll/Vote
        [HttpPost]
        public IActionResult Vote(int pollId, int optionNumber)
        {
            _pollRepository.Vote(pollId, optionNumber);
            return RedirectToAction("Details", new { id = pollId });
        }

        // GET: /Poll/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null) return NotFound();
            return View(poll);
        }

        // POST: /Poll/Edit
        [HttpPost]
        public IActionResult Edit(Poll poll)
        {
            if (ModelState.IsValid)
            {
                _pollRepository.UpdatePoll(poll);
                return RedirectToAction("Index");
            }
            return View(poll);
        }
    }
}
