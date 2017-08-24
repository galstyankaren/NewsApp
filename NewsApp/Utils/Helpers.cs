using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsApp.Models;

namespace NewsApp.Utils
{
    public class Helpers
    {
        /// <summary>
        /// Converts unix time to DateTime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        static DateTime UnixToDateTime(long time)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddSeconds(time);
        }
        static string LevelToCategory(int level) {
            switch (level)
            {

                case 1:
                    return "low";
                case 2:
                    return "medium";
                case 3:
                    return "high";
                default:
                    return "low";
            }

        }

        /// <summary>
        /// Converts Json model to Article model.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static ArticleModels JsonToArticleModel(JsonArticle article, bool isDetail)
        {
            string[] articlesList = article.body.Split('.');

            ArticleModels temp = new ArticleModels()
            {
                body = (isDetail)? article.body : String.Format(articlesList[0] + "." + articlesList[1]+"."), //Checks if the model is going to view or not for body text stripping.
                categories = article.categories,
                id = article.id,
                level = LevelToCategory(article.level),
                url_action = article.url_action,
                published_date = Helpers.UnixToDateTime(article.published_date), //Converts Unix TimeStamp to DateTime
                title = article.title,
                url_explanation = article.url_explanation
            };
            return temp;
        }
    }
}