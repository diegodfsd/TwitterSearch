using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;


namespace TwitterSearch
{
    public class TwitterSearchF
    {
        private const string urlSearch = "http://search.twitter.com/search.atom?{0}";
        private readonly TwitterXmlParser _parser;
        private readonly Dictionary<string, string> _query = new Dictionary<string, string>();

        public TwitterSearchF(TwitterXmlParser parser)
        {
            _parser = parser;
        }

        public IList<string> Search()
        {
            using (var stream = new WebClient().OpenRead(BuildUrl()))
            {
                return _parser
                    .Deserialize(stream)
                    .ToList();
            }
        }

        public TwitterSearchF Query(string query)
        {
            _query["q"] = query;
            return this;
        }

        public TwitterSearchF Rpp(int quantity)
        {
            _query["rpp"] = quantity.ToString();
            return this;
        }

        private string BuildUrl()
        {
            string query = _query.Aggregate(string.Empty, 
                (current, p) => current + string.Format("{0}={1}&", p.Key, p.Value));
            return string.Format(urlSearch, query.Substring(0, query.Length-1));
        }
    }
}