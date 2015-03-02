using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using LinqToTwitter;

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

        public List<Post> GetPosts(string topic)
        {
            var queryResults =
              from search in _twitterCtx.Search
              where search.Type == SearchType.Search &&
                    search.Query == GetQueryTextForTopic(topic) &&
                    search.Count == 50 &&
                    search.SearchLanguage == "en" &&
                    search.IncludeEntities == false &&
                    search.ResultType == ResultType.Recent
              select search;

            Search searchResults = queryResults.Single();

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

        private string GetQueryTextForTopic(string topic)
        {
            string queryText = string.Empty;

            switch (topic)
            {
                case "f12": // F12
                    queryText = @"f12 OR tools OR debugging IE10 OR IE11 OR ""IE 11"" OR ""Internet Explorer""";
                    break;
                case "alt": // ALT
                    queryText = @"""Visual Studio"" ultimate OR debug OR lens OR sense OR map OR profiler OR intellitrace OR devops OR progression OR hub OR ""unit tests""";
                    break;
                case "xdt": // Xaml Design Tools
                    queryText = @"blend ""Visual Studio"" OR xaml OR microsoft OR unnir";
                    break;
                case "hdt": // Html Design Tools
                    queryText = @"blend ""Visual Studio"" OR html OR microsoft";
                    break;
                case "chakra": // Chakra runtime
                    queryText = @"javascript chakra OR v8 OR ""Internet Explorer"" OR IE10 OR IE11 OR carakan OR tamarin OR monkey OR nitro OR jsrt";
                    break;
                case "jstools": // JavaScript tools
                    queryText = @"""Visual Studio"" javascript OR js";
                    break;
                case "typescript": // TypeScript
                    queryText = @"typescript";
                    break;
                case "cat": // CAT team
                    queryText = @"""Visual Studio"" azure OR packaging OR store OR phone OR emulator OR manifest OR publish";
                    break;
                case "visualstudio": // Visual Studio
                    queryText = @"""Visual Studio"" OR VS2012 OR VS2013";
                    break;
                case "appinsights": // Application Insights
                    queryText = @"""application insights"" OR ""app insights""";
                    break;
                case "fiddler": // Fiddler 
                    queryText = @"fiddler browser OR network OR IE OR Chrome OR web OR request OR Firefox OR port OR mono OR mac OR xamarin OR telerik";
                    break;
                case "dev14": // Visual Studio 2015 (Dev14)
                    queryText = @"""visual studio"" OR vs AND 2015 ctp";
                    break;
            }

            queryText += " exclude:retweets";

            return queryText;
        }
    }
}
