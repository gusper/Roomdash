using Engine.Models;
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
            Authenticate();
        }

        public List<Post> GetPosts(TopicModel requestedTopic)
        {
            //List<Post> fullResults = new List<Post>();
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
            return null;
        }

        private string ExtractRelevantContent()
        {
            //string content;

            //if (!string.IsNullOrWhiteSpace(activity.Title))
            //{
            //    content = activity.Title;
            //}
            //else if (activity.Object.Attachments.Count > 0)
            //{
            //    content = activity.Object.Attachments[0].Content;
            //}
            //else if (!string.IsNullOrWhiteSpace(activity.Object.Content))
            //{
            //    content = activity.Object.Content;
            //}
            //else
            //{
            //    content = "Unknown content.";
            //}

            //if (content.Length > 100)
            //{
            //    content = content.Substring(0, 100) + "…";
            //}

            //return content;
            return String.Empty;
        }

        private void Authenticate()
        {
            //// Create the service and authenticate using API key
            //_plusService = new PlusService(new BaseClientService.Initializer()
            //{
            //    ApiKey = Secrets.ApiKeys.GooglePlusApiKey, 
            //    ApplicationName = Secrets.ApiKeys.GooglePlusApplicationName
            //});
        }
    }
}