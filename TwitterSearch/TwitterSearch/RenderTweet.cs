using System;
using System.Collections.Generic;
using System.IO;

namespace TwitterSearch
{
    public class RenderTweet
    {
        private readonly IList<string> _tweets;

        public RenderTweet(IList<string> tweets)
        {
            _tweets = tweets;
        }

        public void Write(TextWriter writer)
        {
            foreach (var tweet in _tweets)
            {
                Console.WriteLine("tweet:{0}", tweet);
                Console.WriteLine("----------------------------------");
            }
        }
    }
}