using Engine.Models;
using LinqToTwitter;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Engine
{
    public class TwitterPostProvider : IPostProvider
    {
        private TwitterContext _twitterCtx;

        public void Connect()
        {
            IOAuthCredentials credentials = new InMemoryCredentials()
            {
                ConsumerKey = Secrets.ApiKeys.TwitterConsumerKey,
                ConsumerSecret = Secrets.ApiKeys.TwitterConsumerSecret,
                OAuthToken = Secrets.ApiKeys.TwitterOAuthToken,
                AccessToken = Secrets.ApiKeys.TwitterAccesstoken
            };

            _twitterCtx = new TwitterContext(new SingleUserAuthorizer() { Credentials = credentials });
        }

        public List<Post> GetPosts(TopicModel requestedTopic)
        {
            var queryResults =
              from search in _twitterCtx.Search
              where search.Type == SearchType.Search &&
                    search.Query == requestedTopic.TwitterQuery + " exclude:retweets" &&
                    search.Count == 50 &&
                    search.SearchLanguage == "en" &&
                    search.IncludeEntities == false &&
                    search.ResultType == ResultType.Recent
              select search;

            Search searchResults = null;

            try
            {
                searchResults = queryResults.Single();
            }
            catch (LinqToTwitter.TwitterQueryException)
            {
                Debug.WriteLine("error from LinqToTwitter");
                return new List<Post>() {new Post()
                {
                    SourceService = "twitter",
                    ScreenName = "",
                    Name = "Twitter",
                    //DateCreated = status.CreatedAt,
                    //ID = status.StatusID,
                    Text = "Failing to get data from Twitter.",
                    UrlToUserProfile = "http://twitter.com/",
                    UrlToPost = "http://twitter.com/",
                    //UrlToUserAvatar = status.User.ProfileImageUrl,
                    FollowersCount = 0,
                } };
            }

            return searchResults.Statuses.Select(status => new Post()
            {
                SourceService = "twitter",
                ScreenName = status.User.Identifier.ScreenName,
                Name = status.User.Name,
                DateCreated = status.CreatedAt,
                ID = status.StatusID,
                Text = status.Text,
                UrlToUserProfile = "http://twitter.com/" + status.User.Identifier.ScreenName,
                UrlToPost = "http://twitter.com/" + status.User.Identifier.ScreenName + "/status/" + status.StatusID,
                UrlToUserAvatar = status.User.ProfileImageUrl,
                FollowersCount = status.User.FollowersCount,
            }).ToList();
        }
    }
}
