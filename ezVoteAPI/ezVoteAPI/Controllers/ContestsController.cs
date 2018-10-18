using ezVoteAPI.Services;
using System.Web.Mvc;

namespace ezVoteAPI.Controllers
{
    public class ContestsController : Controller
    {
        private ContestRepository contestRepo;

        public ContestsController()
        {
            this.contestRepo = new ContestRepository();
        }

        public JsonResult GetContests()
        {
            var contests = contestRepo.GetAllContests();
            return Json(contests, JsonRequestBehavior.AllowGet);
        }
    }
}
