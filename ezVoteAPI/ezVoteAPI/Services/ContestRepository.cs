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

        public static List<Issue> LoadCandidateInfo(string name, string Url)
        {
            HttpClient http = new HttpClient();
            var path= Path.Combine(System.IO.Path.GetFullPath(@"..\..\"), @"C:\Users\aagarwal\Documents\GitHub\hack18-ezVoteAPI\ezVoteAPI\ezVoteAPI\Rsources\Urllists");
            string[] filePaths =
                Directory.GetFiles(path, "*.txt");
            List<Issue> myList = new List<Issue>();
            Dictionary<string, string> hash = new Dictionary<string, string>();
            var issueMainUrl = "";

            List<string> IssueKeyWords = new List<string>();
            IssueKeyWords.Add("hurricane");
            IssueKeyWords.Add("educat");
            IssueKeyWords.Add("environment");
            IssueKeyWords.Add("latin");
            IssueKeyWords.Add("health");
            IssueKeyWords.Add("immigration");            
            IssueKeyWords.Add("safe");
            IssueKeyWords.Add("seniors");
            IssueKeyWords.Add("job");



            foreach (var urlList in filePaths)
            {
                var list = urlList.Split('\\');
                var nam = list.Last().Split(('.'));

                if (!Url.Contains(nam.First()))
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

                if (hash.Count >= 2)
                {
                    foreach (var url in hash)
                    {
                        var issueSta = new Issue();
                        Issue issues = new Issue();
                        BrowserSession b = new BrowserSession();
                        var response = b.Get(url.Value);
                        var node = response.SelectSingleNode("//div[@class='floating-container']");
                        if(node != null )
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
                            d =HttpUtility.HtmlDecode(d);
                            



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
                        issueSta.Name = lin.PreviousSibling.InnerText.Replace("\r\n\t", string.Empty); ;

                        //// just add the url attribute and pass it up from here
                       Regex.Replace(lin.InnerText, @"\t|\n|\r", "");
                        issueSta.Text = issueSta.Text + lin.InnerText.Replace("\r\n\\t", string.Empty);;
                        myList.Add(issueSta);
                    }
                }

            }

            return myList;
        }
    }
}