using Apportiswebscrapper;
using ezVoteAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

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
                    Candidates = { rick,bill }
                }
            };
        }

        private static Dictionary<string, string> GetIssuesAndKeywords()
        {
            var IssueKeyWords = new Dictionary<string, string>();
            IssueKeyWords.Add("Education", "educat,children");
            IssueKeyWords.Add("Economy", "job,econom,tax");
            IssueKeyWords.Add("Environment", "environment,climate,resources,wildlife,pollut,solar,wind");
            IssueKeyWords.Add("Healthcare", "health");
            IssueKeyWords.Add("Immigration", "immigration");
            IssueKeyWords.Add("Public Safety", "safety,gun,law,civil");
            IssueKeyWords.Add("Senior Citizens", "seniors,medicare,social-security");
            IssueKeyWords.Add("Latin America", "latin");
            IssueKeyWords.Add("Natural Disaster Preparedness", "hurricane");
            return IssueKeyWords;
        }

        public static List<Issue> LoadCandidateInfo(string name, string Url)
        {
            HttpClient http = new HttpClient();
            List<Issue> myList = new List<Issue>();
            Dictionary<string, string> hash = new Dictionary<string, string>();
            var issueMainUrl = "";

            var IssueKeyWords = GetIssuesAndKeywords();

            var mydata = new DataClass();
            
            foreach (var item in mydata.data)
            {
                if (Url.Contains(item.Key))
                {

                    foreach (var line in item.Value)
                    {
                        foreach (var kvp in IssueKeyWords)
                        {
                            foreach (var keyword in kvp.Value.Split(','))
                            {
                                if (line.Contains(keyword))
                                {
                                    if (!hash.ContainsKey(kvp.Key))
                                        hash.Add(kvp.Key, line);
                                }

                                if (line.Contains("issue"))
                                {
                                    issueMainUrl = line;
                                }
                            }
                        }
                    }

                    if (hash.Count >= 2)
                    {
                        foreach (var url in hash)
                        {
                            var issueSta = new Issue();
                            Issue issues = new Issue();
                            BrowserSession b = new BrowserSession();
                            var response = b.Get(url.Value);
                            var node = response.SelectSingleNode("//div[@class='floating-container']");
                            if (node != null)
                                node.Remove();
                            node = response.SelectSingleNode("//div[@class='row alert-bar__row']");
                            if (node != null)
                                node.Remove();

                            foreach (HtmlAgilityPack.HtmlNode lin in response.SelectNodes("//p"))
                            {
                                int i = 0;

                                string d = Encoding.ASCII.GetString(
                                    Encoding.Convert(
                                        Encoding.UTF8,
                                        Encoding.GetEncoding(
                                            Encoding.ASCII.EncodingName,
                                            new EncoderReplacementFallback(string.Empty),
                                            new DecoderExceptionFallback()
                                        ),
                                        Encoding.UTF8.GetBytes(lin.InnerText)
                                    )
                                );
                                //string inputString = "Räksmörgås";
                                d = lin.InnerText.Replace("\r", string.Empty);
                                d = d.Replace("\t", string.Empty);
                                //d = lin.InnerText.Replace("\u", string.Empty);
                                d = d.Replace("\n", string.Empty);
                                d = d.Replace("\u0027ve", string.Empty);
                                d = d.Replace("\\", string.Empty);
                                d = HttpUtility.HtmlDecode(d);

                                issueSta.Text = issueSta.Text + d;
                                i++;
                                issueSta.Name = url.Key;
                                if (i == 2)
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
                        var node = response.SelectSingleNode("//div[@class='floating-container']");
                        if (node != null)
                            node.Remove();
                        node = response.SelectSingleNode("//div[@class='row alert-bar__row']");
                        if (node != null)
                            node.Remove();

                        foreach (HtmlAgilityPack.HtmlNode lin in response.SelectNodes("//p"))
                        {
                            var header = string.Empty;
                            if (lin.PreviousSibling == null)
                            {
                                continue;
                            }

                            var issueSta = new Issue();
                            issueSta.Name = lin.PreviousSibling.InnerText.Replace("\r\n\t", string.Empty);

                            //// just add the url attribute and pass it up from here
                            Regex.Replace(lin.InnerText, @"\t|\n|\r", "");
                            issueSta.Text = issueSta.Text + lin.InnerText.Replace("\r\n\\t", string.Empty);
                            ;
                            myList.Add(issueSta);
                        }
                    }
                }

            }


            PrettyFormat(myList);

            return myList;
        }

        private static void PrettyFormat(List<Issue> issues)
        {
            var allIssues = GetIssuesAndKeywords();
            foreach (var issue in allIssues.Keys)
            {
                if (!issues.Any(i => i.Name == issue))
                    issues.Add(new Issue {Name = issue, Text = "Our search did not find any results"});
            }

            issues.Sort((p, q) => p.Name.CompareTo(q.Name));
        }
    }
}