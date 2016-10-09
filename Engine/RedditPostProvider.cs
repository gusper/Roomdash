using Engine.Models;
using Engine.Secrets;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class RedditPostProvider : IPostProvider
    {
        private Reddit _reddit;

        public void Connect()
        {
            _reddit = new Reddit();
            Authenticate();
        }

        public List<Post> GetPosts(TopicModel topic)
        {
            var results = new List<Post>();

            foreach (var subredditName in topic.RedditSubreddits)
            {
                var subreddit = _reddit.GetSubreddit(subredditName);
                var subResults = subreddit.Search(topic.RedditQuery, Sorting.New, TimeSorting.Year).Take(5);

                foreach (var post in subResults)
                {
                    results.Add(new Post()
                    {
                        SourceService = "reddit",
                        Text = post.Title,
                        Name = post.AuthorName,
                        UrlToUserProfile = $@"http://reddit.com/user/{post.AuthorName}",
                        DateCreated = post.Created,
                        UrlToPost = $@"http://reddit.com/{post.Permalink.OriginalString}",
                    });
                }
            }

            return results;
        }

        private void Authenticate()
        {
            _reddit.LogIn(ApiKeys.RedditUserName, ApiKeys.RedditPassword);
        }
    }
}