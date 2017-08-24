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
        public ActionResult Index()
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

                ArticleModels temp = new ArticleModels()
                {
                    body = article.body,
                    categories = article.categories,
                    id = article.id,
                    level = article.level,
                    url_action = article.url_action,
                    published_date = Helpers.UnixToDateTime(article.published_date), //Converts Unix TimeStamp to DateTime
                    title = article.title,
                    url_explanation = article.url_explanation
                };
                articleList.Add(temp);
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

            ArticleModels temp = new ArticleModels()
            {
                body = JsonArticle.body,
                categories = JsonArticle.categories,
                id = JsonArticle.id,
                level = JsonArticle.level,
                url_action = JsonArticle.url_action,
                published_date = Helpers.UnixToDateTime(JsonArticle.published_date),
                title = JsonArticle.title,
                url_explanation = JsonArticle.url_explanation
            };

            return View(temp);
        }
    }
}