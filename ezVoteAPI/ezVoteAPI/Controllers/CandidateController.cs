using ezVoteAPI.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace ezVoteAPI.Controllers
{
    public class CandidateController : Controller
    {
        private CandidateRepository candidateRepo;

        public CandidateController()
        {
            this.candidateRepo = new CandidateRepository();
        }

        //public Candidate[] Get()
        //{
        //    return candidateRepo.GetAllCandidates();
        //}

        public JsonResult GetCandidates()
        {
            var candidates = candidateRepo.GetAllCandidates();
            return Json(candidates, JsonRequestBehavior.AllowGet);
        }
    }
}
