using ezVoteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ezVoteAPI.Services
{
    public class ApiDataRetriever
    {
        private static HttpClient client = new HttpClient();

        private static async Task<Contest> GetContestsAsync(string path)
        {
            Contest contest = null;
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                contest = await response.Content.ReadAsAsync<Contest>();
            }
            return contest;
        }

        //public static async Task RunAsync()
        //{
        //    client.BaseAddress = new Uri("https://apis.google.com/js/client.js?onload=load");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //}
    }
}