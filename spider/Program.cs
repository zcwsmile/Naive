using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace spider
{
    class Program
    {
        static HashSet<String> articleLinks = new HashSet<String>();
        static Queue<String> otherLinks = new Queue<String>();
        static HashSet<String> usedEntries = new HashSet<String>();
        static Regex articleLinkRegex = new Regex("/articles/\\d{6}.htm");
        static Regex otherLinkRegex = new Regex("http://www.cnbeta.com/topics/\\d+.htm");

        static void ParseUrl(String url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            String webPageContent = reader.ReadToEnd();
            MatchCollection results = articleLinkRegex.Matches(webPageContent);
            foreach (Match result in results)
            {
                articleLinks.Add("http://www.cnbeta.com" + result.ToString());
            }
            results = otherLinkRegex.Matches(webPageContent);
            foreach (Match result in results)
            {
                String str = result.ToString();
                if (!usedEntries.Contains(str))
                {
                    otherLinks.Enqueue(str);
                    usedEntries.Add(str);
                }
            }
        }

        static void Main(string[] args)
        {
            ParseUrl("http://www.cnbeta.com");
            while ((articleLinks.Count > 100) || (otherLinks.Count < 0))
            {
                ParseUrl(otherLinks.Dequeue());
            }
            using (FileStream fs = new FileStream("d:/articles.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (String str in articleLinks)
                    {
                        sw.WriteLine(str);
                    }
                }
            }
        }
    }
}
