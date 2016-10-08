using System.Collections.Generic;

namespace Engine.Models
{
    public class TopicModel
    {
        // General
        public string Name { get; set; }
        public string UrlSlug { get; set; }

        // Twitter
        public string TwitterQuery { get; set; }

        // Google+
        public string GooglePlusQuery { get; set; }

        // StackOverflow
        public string StackOverflowTagged { get; set; }
        public string StackOverflowNotTagged { get; set; }
        public string StackOverflowInTitle { get; set; }

        // Reddit
        public string RedditQuery { get; set; }
        public List<string> RedditSubreddits { get; set; }
    }
}
