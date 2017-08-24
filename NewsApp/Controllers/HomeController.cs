using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsApp.Models;
using Newtonsoft.Json;
using System.Text;

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


            return View(root.articles);
        }

        /// <summary>
        /// Details view
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public ActionResult Details(int _id)
        {
            return View();
        }
    }
}