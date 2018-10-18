using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezVoteAPI.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Stance { get; set; }
        public string Url { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}