using ezVoteAPI.Models;

namespace ezVoteAPI.Services
{
    public class ContestRepository
    {
        public Contest[] GetAllContests()
        {
            var rick = new Candidate
            {
                Name = "Rick Scott",
                Party = "Republican Party",
                Issues =
                {
                    new Issue { Name = "Bio", Stance = "I am Rick" },
                    new Issue { Name = "Education", Stance = "I support education" }
                }
            };
            var bill = new Candidate
            {
                Name = "Bill Nelson",
                Party = "Democratic Party",
                Issues =
                {
                    new Issue { Name = "About Me", Stance = "My name's not Rick" },
                    new Issue { Name = "Education", Stance = "I like education" }
                }
            };
            return new Contest[]
            {
                new Contest
                {
                    Id = 1,
                    Name = "US Senator",
                    Candidates = { rick, bill }
                }
            };
        }
    }
}