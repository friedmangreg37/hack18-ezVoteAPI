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
        public string Bio { get; set; }

        public int RaceId { get; set; }
        public ElectionRace Race { get; set; }

        public ICollection<IssueStance> Issues { get; set; }
    }
}