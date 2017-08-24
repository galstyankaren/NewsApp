using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;

using NewsApp.Utils;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        //Endpoint to the NEws API
        private static string ENDPOINT = "https://www.sicher-im-netz.de/siba-app/siba/list/0";

        /// <summary>
        /// Home Page (Master view) of the app. Returns a list of articles from the NEWS API.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string sortOrder,string filterByDate, int? filterByLevel)
        {

            Rootobject root = new Rootobject();

            using (var webClient = new System.Net.WebClient())
            {
                //Specified encoding to avoid issues related to German language characters
                webClient.Encoding = Encoding.UTF8;

                var json = webClient.DownloadString(ENDPOINT);

                //Not directly deserialized to Rootobject as the JSON is in the [{}] form.
                root.articles = JsonConvert.DeserializeObject<List<JsonArticle>>(json);
            }

            List<ArticleModels> articleList = new List<ArticleModels>();


            foreach (JsonArticle article in root.articles)
            {

                ArticleModels temp = Helpers.JsonToArticleModel(article);
                articleList.Add(temp);
            }

            switch (sortOrder)
            {
                case "level":
                    articleList = articleList.OrderByDescending(s => s.level).ToList();
                    break;
                case "title":
                    articleList = articleList.OrderBy(s => s.title).ToList(); //Assending Order for titles.
                    break;
                case "date":
                    articleList = articleList.OrderByDescending(s => s.published_date).ToList();
                    break;
                default:
                    articleList = articleList.OrderByDescending(s => s.id).ToList();
                    break;
            }

            if (!String.IsNullOrEmpty(filterByDate))
            {
                DateTime date;
                if(DateTime.TryParse(filterByDate, out date)) //Avoid invalid DateTimeInput
                //  articleList = articleList.Where(x => x.published_date==date).ToList(); //TODO: "Contains" for DateTime Comparision
                    articleList = articleList.Where(x => x.published_date.ToString().Contains(filterByDate)).ToList(); //TODO: Unuglify
            }
            if (filterByLevel!=null)
            {
                articleList = articleList.Where(x => x.level == filterByLevel).ToList();
            }

            return View(articleList);
        }

        /// <summary>
        /// Details view for a specific article
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public ActionResult Details(int _id)
        {
            JsonArticle JsonArticle = new JsonArticle();
            using (var webClient = new System.Net.WebClient())
            {
                //Specified encoding to avoid issues related to German language characters
                webClient.Encoding = Encoding.UTF8;

                var json = webClient.DownloadString(ENDPOINT);

                //Deserializes to a List of Json Articles and queries an article with matching ID.
                JsonArticle = JsonConvert.DeserializeObject<List<JsonArticle>>(json).Where(x=>x.id==_id).FirstOrDefault();
            }

            ArticleModels temp = Helpers.JsonToArticleModel(JsonArticle);

            return View(temp);
        }
    }
}