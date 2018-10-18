using Apportiswebscrapper;
using ezVoteAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

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
                Issues = LoadCandidateInfo("Rick Scott", @"https://rickscottforflorida.com/")
            };
            var bill = new Candidate
            {
                Name = "Bill Nelson",
                Party = "Democratic Party",
                Issues = LoadCandidateInfo("Bill Nelson", @"https://www.nelsonforsenate.com/")
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

        public static List<Issue> LoadCandidateInfo(string name, string Url)
        {
            HttpClient http = new HttpClient();
            string[] filePaths =
                Directory.GetFiles(@"C:\hack18-ezVoteAPI\ezVoteAPI\Urllists", "*.txt");
            List<Issue> myList = new List<Issue>();
            Dictionary<string, string> hash = new Dictionary<string, string>();
            var issueMainUrl = "";

            List<string> IssueKeyWords = new List<string>();
            IssueKeyWords.Add("hurricane");
            IssueKeyWords.Add("education");
            IssueKeyWords.Add("environment");
            IssueKeyWords.Add("latin");
            IssueKeyWords.Add("healthcare");
            IssueKeyWords.Add("immigration");
            IssueKeyWords.Add("veterans");
            IssueKeyWords.Add("safety");


            foreach (var urlList in filePaths)
            {
                if (!Url.Contains(urlList))
                {
                    continue;
                }

                var lines = System.IO.File.ReadLines(urlList);
                foreach (var line in lines)
                {
                    foreach (var kyword in IssueKeyWords)
                    {
                        if (line.Contains(kyword))
                        {
                            if (!hash.ContainsKey(kyword))
                                hash.Add(kyword, line);
                        }

                        if (line.Contains("issue"))
                        {
                            issueMainUrl = line;
                        }
                    }
                }

                if (hash.Count > 3)
                {
                    foreach (var url in hash)
                    {
                        var issueSta = new Issue();
                        Issue issues = new Issue();
                        BrowserSession b = new BrowserSession();
                        var response = b.Get(url.Value);
                        foreach (HtmlAgilityPack.HtmlNode lin in response.SelectNodes("//p"))
                        {
                            int i = 0;

                            issueSta.Text = issueSta.Text + lin.InnerText;
                            i++;
                            issueSta.Name = url.Key;
                            if (i == 3)
                                break;

                        }

                        myList.Add(issueSta);
                    }
                }
                else
                {

                    Issue issues = new Issue();
                    BrowserSession b = new BrowserSession();
                    var response = b.Get(issueMainUrl);
                    foreach (HtmlAgilityPack.HtmlNode lin in response.SelectNodes("//p"))
                    {

                        var header = string.Empty;
                        if (lin.PreviousSibling == null)
                        {
                            continue;
                        }

                        var issueSta = new Issue();

                        issueSta.Name = lin.PreviousSibling.InnerText;

                        //// just add the url attribute and pass it up from here
                        issueSta.Text = issueSta.Text + lin.InnerText;
                        myList.Add(issueSta);



                    }
                }

            }

            return myList;
        }
    }
}