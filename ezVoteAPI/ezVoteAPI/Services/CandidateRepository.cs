using ezVoteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezVoteAPI.Services
{
    public class CandidateRepository
    {
        public Candidate[] GetAllCandidates()
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