using ezVoteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ezVoteAPI.Controllers
{
    public class CandidateController : ApiController
    {
        public Candidate[] Get()
        {
            return new Candidate[]
            {
                new Candidate
                {
                    Id = 1,
                    Name = "John Doe",
                    Bio = "I am running for office"
                },
                new Candidate
                {
                    Id = 2,
                    Name = "Joe Schmo",
                    Bio = "I am also running for office"
                }
            };
        }
    }
}
