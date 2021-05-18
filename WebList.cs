using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Web;

namespace WebsiteForWikiToPhil.Models
{
    public class WebList
    {
        public WebList()
        {
            firstLink = "https://en.wikipedia.org/wiki/Science";
            oldTitles = "";
        }

        public string firstLink { get; set; }

        public string oldTitles { get; set; }

        public string answer()
        {
            if (foundPhil) return "Found Philosophy!";
            if (loop) return "In a Loop!";
            return "In a dead end!";
        }

        public List<string> getLinks()
        {
            createList();
            return Titles;
        }

        public List<string> Titles;

        private bool foundPhil;

        private bool loop;

        private bool deadend;

        public void createList()
        {
            foundPhil = false;
            loop = false;
            deadend = false;
            string start = firstLink; //first link or starting point
            Titles = new List<string>();
            bool duplicate = false;

            while (!duplicate)
            {
                string html = @start;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument htmlDoc = web.Load(html);
                HtmlNodeCollection body = htmlDoc.DocumentNode.SelectNodes("//body"); //load body of html

                string title1 = htmlDoc.DocumentNode.SelectSingleNode("//head/title").InnerHtml;
                title1 = title1.Replace("- Wikipedia", "");
                Titles.Add(title1);

                if (title1 == "Philosophy ") //page is philosophy
                {
                    foundPhil = true;
                    break;
                }
                else
                {
                    for (int i = 0; i < Titles.Count - 1; ++i) //search if page has been visited already
                    {
                        if (title1 == Titles[i])
                        {
                            loop = true;
                            duplicate = true;
                            break;
                        }
                    }
                }

                bool found = false; //bool for found first link
                foreach (HtmlNode ptag in body.Descendants("p"))
                {
                    foreach (HtmlNode atag in ptag.Descendants("a"))
                    {
                        string script = ptag.InnerHtml; //get html script in <p> tag
                        string newLink = atag.GetAttributeValue("href", "null");

                        int open = 0, close = 0; //position of open and closed bracket
                        int startLink = 0; //position of link
                        bool trashLink = false; //link is in enclosed brackets

                        while (script.Contains("(") && script.Contains(")"))
                        {
                            startLink = script.IndexOf(newLink);
                            open = script.IndexOf("(");
                            close = script.IndexOf(")");

                            if (startLink > open && startLink < close)
                            {
                                trashLink = true;
                                break;
                            }
                            else if (startLink > close)
                            {
                                script = script.Remove(open - 1, 2); //delete open bracket
                                script = script.Remove(close - 3, 2); //delete close bracket

                            }
                            else if (startLink < open)
                            {
                                break;
                            }
                        }

                        if (trashLink)
                        {
                            continue;
                        }

                        if (newLink.Length > 6) //link must be minimum of 7 characters long
                        {
                            if (newLink == "/wiki/Wikipedia:Shortcut") //get rid of this link
                            {
                                continue;
                            }
                            string validLink = newLink.Substring(0, 6);
                            if (validLink == "/wiki/")
                            {
                                start = "https://en.wikipedia.org" + newLink; //set start link to new link
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
                if (!found)
                {
                    deadend = true;
                    break;
                }
            }
        }
        
    }
}
