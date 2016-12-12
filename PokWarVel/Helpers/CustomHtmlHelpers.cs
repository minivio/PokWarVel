using PokWarVel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokWarVel.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString List(this HtmlHelper html, List<string> listItems){
            TagBuilder tag = new TagBuilder("ul");
            foreach(string item in listItems){
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText(item);
                tag.InnerHtml += itemTag.ToString();
            }
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString List(this HtmlHelper html, string[] listItems)
        {
            TagBuilder tag = new TagBuilder("ul");
            foreach (string item in listItems)
            {
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText(item);
                tag.InnerHtml += itemTag.ToString();
            }
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString List(this HtmlHelper html, List<PopularModel> listItems)
        {
            TagBuilder tag = new TagBuilder("ul");
            int cpteur = 1;
            foreach (PopularModel item in listItems)
            {
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText("Number " + cpteur.ToString() + ": " + item.NomHero + " - cote de popularité: " + item.RatingHero.ToString());
                tag.InnerHtml += itemTag.ToString();
                cpteur++;
            }
            return new MvcHtmlString(tag.ToString());
        }
    }
}