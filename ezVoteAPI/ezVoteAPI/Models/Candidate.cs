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
        public string Bio { get; set; }
    }
}