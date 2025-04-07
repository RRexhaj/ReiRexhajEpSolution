using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ReiRexhajEpSolution.Presentation.Filters
{
    public class OneVotePerPollFilter : IActionFilter
    {
        private string _pollId; // store pollId here to use in OnActionExecuted

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Access ActionArguments here, since ActionExecutingContext has them.
            if (context.ActionArguments.ContainsKey("pollId"))
            {
                _pollId = context.ActionArguments["pollId"]?.ToString();

                // Example check: did user already vote?
                var user = context.HttpContext.Session.GetString("User");
                var votedPolls = context.HttpContext.Session.GetString("VotedPolls");
                if (string.IsNullOrEmpty(user))
                {
                    // Force login if user not logged in
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                    return;
                }
                if (!string.IsNullOrEmpty(votedPolls) && votedPolls.Contains(_pollId))
                {
                    context.Result = new ContentResult { Content = "You have already voted in this poll." };
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // ActionExecutedContext does NOT have ActionArguments, so we use _pollId from earlier.
            if (!string.IsNullOrEmpty(_pollId))
            {
                var votedPolls = context.HttpContext.Session.GetString("VotedPolls") ?? "";
                votedPolls += _pollId + ",";
                context.HttpContext.Session.SetString("VotedPolls", votedPolls);
            }
        }
    }
}
