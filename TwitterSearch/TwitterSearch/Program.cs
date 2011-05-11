using System;
using System.IO;
using System.Net;

namespace TwitterSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            const string query = "http://search.twitter.com/search.json?q=mvc3";
            WebRequest webRequest = WebRequest.Create(query);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";

            using (var response = webRequest.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    var contentReader = new StreamReader(stream);
                    var content = contentReader.ReadToEnd();
                    dynamic dtweets = JsonParser.JsonParser.Deserialize(content);

                    foreach (var tweet in dtweets.results)
                    {
                        Console.WriteLine("Creado:{0}", tweet["created_at"]);
                        Console.WriteLine("From:{0}", tweet["from_user"]);
                        Console.WriteLine("Texto:{0}", tweet["text"]);
                        Console.WriteLine("***********************************************************");
                    }
                }
            }


            /*DEPOIS DO REFACTORING*/
            var parser = new TwitterXmlParser();
            var tweets = new TwitterSearchF(parser)
                .Query("mvc3")
                .Rpp(20)
                .Search();

            var render = new RenderTweet(tweets);
            render.Write(Console.Out);

            Console.ReadLine();
        }
    }
}
