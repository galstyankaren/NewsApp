using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsApp.Models
{

    public class Rootobject
    {
        public List<JsonArticle> articles { get; set; }
    }

    public class JsonArticle
    {
        public string published_date { get; set; }
        public int level { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string[] categories { get; set; }
        public Url_Action[] url_action { get; set; }
        public Url_Explanation[] url_explanation { get; set; }
        public int id { get; set; }
    }

    public class Url_Action
    {
        public string url { get; set; }
        public string title { get; set; }
    }

    public class Url_Explanation
    {
        public string url { get; set; }
        public string title { get; set; }
    }

}