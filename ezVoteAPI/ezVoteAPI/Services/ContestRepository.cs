using ezVoteAPI.Models;

namespace ezVoteAPI.Services
{
    public class ContestRepository
    {
        public Contest[] GetAllContests()
        {
            return new Contest[]
            {
                new Contest
                {
                    Id = 1,
                    Name = "US Senator"
                },
                new Contest
                {
                    Id = 2,
                    Name = "US Representative"
                }
            };
        }
    }
}