using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezVoteAPI.Models
{
    public class Contest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        public Contest()
        {
            Candidates = new List<Candidate>();
        }
    }
}