//using Engine.Models;
//using Engine.Secrets;
//using RedditSharp;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Engine
//{
//    public class RedditPostProvider : IPostProvider
//    {
//        private Reddit _reddit;

//        public void Connect()
//        {
//            _reddit = new Reddit();
//            Authenticate();
//        }

//        public List<Post> GetPosts(TopicModel topic)
//        {
//            var results = new List<Post>();

//            foreach (var subredditName in topic.RedditSubreddits)
//            {
//                var subreddit = _reddit.GetSubredditAsync(subredditName);
//// TODO:                 var subResults = subreddit.Search(topic.RedditQuery, Sorting.New, TimeSorting.Year).Take(10);

//            //    try
//            //    {
//            //        foreach (var post in subResults)
//            //        {
//            //            results.Add(new Post()
//            //            {
//            //                SourceService = "reddit",
//            //                Text = post.Title,
//            //                Name = post.AuthorName,
//            //                UrlToUserProfile = $@"http://reddit.com/user/{post.AuthorName}",
//            //                DateCreated = post.Created.UtcDateTime,
//            //                UrlToPost = $@"http://reddit.com/{post.Permalink.OriginalString}",
//            //            });
//            //        }
//            //    }
//            //    catch (System.Net.WebException)
//            //    {
//            //        results.Add(new Post()
//            //        {
//            //            SourceService = "reddit",
//            //            Text = "Reddit API query failed.",
//            //            Name = "Reddit",
//            //            UrlToUserProfile = "",
//            //            DateCreated = DateTime.Now,
//            //            UrlToPost = "",
//            //        });
//            //    }
//            }

//            return results;
//        }

//        private void Authenticate()
//        {
//            //_reddit.LogIn(ApiKeys.RedditUserName, ApiKeys.RedditPassword);
//        }
//    }
//}