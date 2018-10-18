using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezVoteAPI.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Party { get; set; }
        public string Url { get; set; }

        public int RaceId { get; set; }
        public Contest Race { get; set; }

        public ICollection<Issue> Issues { get; set; }

        public Candidate()
        {
            Issues = new List<Issue>();
        }
    }
}