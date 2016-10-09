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
            #region Old code
            //ActivitiesResource.SearchRequest searchRequest;
            //string pageToken = string.Empty;

            //searchRequest = _plusService.Activities.Search(requestedTopic.GooglePlusQuery);

            //while (fullResults.Count < 20)
            //{
            //    searchRequest.PageToken = pageToken;
            //    var activityFeed = searchRequest.Execute();
            //    pageToken = activityFeed.NextPageToken;

            //    var pageOfResults = activityFeed.Items.Where(activity => activity.Title.Length > 0).Select(status => new Post()
            //    {
            //        SourceService = "googleplus",
            //        ScreenName = status.Actor.DisplayName ?? string.Empty,
            //        Name = status.Actor.DisplayName ?? string.Empty,
            //        DateCreated = DateTime.Parse(status.Updated),
            //        UrlToPost = status.Url,
            //        UrlToUserProfile = status.Actor.Url,
            //        Text = ExtractRelevantContent(status),
            //        UrlToUserAvatar = status.Actor.Image.Url ?? string.Empty,
            //    }).ToList();
            //    fullResults.AddRange(pageOfResults);

            //    if (string.IsNullOrEmpty(pageToken))
            //        break;
            //}
            //return fullResults;

            #endregion

            var results = new List<Post>();

            foreach (var subredditName in topic.RedditSubreddits)
            {
                var subreddit = _reddit.GetSubreddit(subredditName);
                var subResults = subreddit.Search(topic.RedditQuery, Sorting.New, TimeSorting.Year).Take(10);
                foreach (var post in subResults)
                {
                    string authorName = "Unknown";
                    string authorUrl = String.Empty;

                    try
                    {
                        if (post.Author != null)
                        {
                            authorName = post.Author.FullName;
                        }
                    }
                    catch {}

                    results.Add(new Post()
                    {
                        SourceService = "reddit",
                        Text = $@"{post.Title} ({subredditName.ToLower()})",
                        Name = authorName,
                        UrlToUserProfile = $@"http://reddit.com/user/{authorName}",
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