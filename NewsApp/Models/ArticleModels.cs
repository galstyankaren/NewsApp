using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsApp.Models
{
    /// <summary>
    /// A Temperory class derived from JsonArticle for Master/Detail Views
    /// </summary>
    public class ArticleModels
    {
        public DateTime published_date { get; set; }
        public string level { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string[] categories { get; set; }
        public Url_Action[] url_action { get; set; }
        public Url_Explanation[] url_explanation { get; set; }
        public int id { get; set; }
    }

}