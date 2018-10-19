using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezVoteAPI
{
    public class DataClass
    {
       public   Dictionary<string, List<String>> data { get; set; }
        public List<string> d { get; set; }
        public List<string> d1 { get; set; }
        public List<string> d2 { get; set; }

        public List<string> d3 { get; set; }
        public List<string> d4 { get; set; }

        public DataClass()
        {
            data = new Dictionary<string, List<string>>();
            d = new List<string>();
            d1= new List<string>();
            d2 =new List<string>();
            this.d.Add("https://www.ennekingforflorida.com");
            this.d.Add("https://www.ennekingforflorida.com/on-the-issues");
            this.d.Add("https://www.ennekingforflorida.com/news");
            this.d.Add("https://www.ennekingforflorida.com/calendar");
            this.d.Add("https://www.ennekingforflorida.com/volunteer");
            this.d.Add("https://www.ennekingforflorida.com/donate");
            data.Add("ennekingforflorida.com.txt",d);
            

            this.d1.Add("http://twitter.com/intent/tweet?text=Contact+-+https%3A%2F%2Fwww.nelsonforsenate.com%2Fcontact%2F+via+%40NelsonForSenate");
            this.d1.Add("https://www.nelsonforsenate.com/bills-story/");
            this.d1.Add("https://www.nelsonforsenate.com/issues/");
            this.d1.Add("https://www.nelsonforsenate.com/news/");
            this.d1.Add("https://www.nelsonforsenate.com/action-center/");
            this.d1.Add("https://www.nelsonforsenate.com//");
            this.d1.Add("https://www.nelsonforsenate.com/issue/economy/");
            this.d1.Add("https://www.nelsonforsenate.com/issue/healthcare/");
            this.d1.Add("https://www.nelsonforsenate.com/issue/environment/");
            this.d1.Add("https://www.nelsonforsenate.com/issue/education/");
            this.d1.Add("https://www.nelsonforsenate.com/issue/consumer-protection/");
            this.d1.Add("https://www.nelsonforsenate.com/issue/seniors/");
            this.d1.Add("https://www.nelsonforsenate.com/category/videos/");
            this.d1.Add("https://www.nelsonforsenate.com/category/press-releases/");

            this.d1.Add("https://www.nelsonforsenate.com/category/press-releases/");
            data.Add("nelsonforsenate.com",d1);
            
           
            this.d2.Add("https://rickscottforflorida.com/meet-rick/");
            this.d2.Add("https://rickscottforflorida.com/");
            this.d2.Add("https://rickscottforflorida.com/news/");
            this.d2.Add("https://rickscottforflorida.com/hurricane-preparedness/");

            this.d2.Add("https://rickscottforflorida.com/education/");

            this.d2.Add("https://rickscottforflorida.com/environment/");
            this.d2.Add("https://rickscottforflorida.com/latin-america/");
            this.d2.Add("https://rickscottforflorida.com/healthcare/");
            this.d2.Add("https://rickscottforflorida.com/immigration/");
            this.d2.Add("https://rickscottforflorida.com/infrastructure/");
            this.d2.Add("https://rickscottforflorida.com/israel/");
            this.d2.Add("https://rickscottforflorida.com/jobs/");
            this.d2.Add("https://rickscottforflorida.com/make-washington-work/");
            this.d2.Add("https://rickscottforflorida.com/public-safety/");

            this.d2.Add("https://rickscottforflorida.com/military-and-veterans/");

            this.data.Add("rickscottforflorida.com",d2);

        }
        
       


    }
}