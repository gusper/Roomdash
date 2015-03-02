using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Services;
using LinqToTwitter;

namespace Engine
{
    public class GooglePlusPostProvider : IPostProvider
    {
        private PlusService _plusService;

        public void Connect()
        {
            Authenticate();
        }

        public List<Post> GetPosts(string topic)
        {
            List<Post> fullResults = new List<Post>();
            ActivitiesResource.SearchRequest searchRequest;
            string pageToken = string.Empty;
            
            searchRequest = _plusService.Activities.Search(GetQueryTextForTopic(topic));
          
            while (fullResults.Count < 20)
            {
                searchRequest.PageToken = pageToken;
                var activityFeed = searchRequest.Execute();
                pageToken = activityFeed.NextPageToken;

                var pageOfResults = activityFeed.Items.Where(activity => activity.Title.Length > 0).Select(status => new Post()
                {
                    SourceService = "googleplus",
                    ScreenName = status.Actor.DisplayName ?? string.Empty,
                    Name = status.Actor.DisplayName ?? string.Empty,
                    DateCreated = DateTime.Parse(status.Updated),
                    UrlToPost = status.Url,
                    UrlToUserProfile = status.Actor.Url,
                    Text = ExtractRelevantContent(status),
                    UrlToUserAvatar = status.Actor.Image.Url ?? string.Empty,
                }).ToList();
                fullResults.AddRange(pageOfResults);

                if (string.IsNullOrEmpty(pageToken))
                    break;
            }
            return fullResults;
        }

        private string ExtractRelevantContent(Activity activity)
        {
            string content;

            if (!string.IsNullOrWhiteSpace(activity.Title))
            {
                content = activity.Title;
            }
            else if (activity.Object.Attachments.Count > 0)
            {
                content = activity.Object.Attachments[0].Content;
            }
            else if (!string.IsNullOrWhiteSpace(activity.Object.Content))
            {
                content = activity.Object.Content;
            }
            else
            {
                content = "Unknown content.";
            }

            if (content.Length > 100)
            {
                content = content.Substring(0, 100) + "…";
            }

            return content;
        }

        private void Authenticate()
        {
            // Create the service and authenticate using API key
            _plusService = new PlusService(new BaseClientService.Initializer()
            {
                ApiKey = Secrets.ApiKeys.GooglePlusApiKey, 
                ApplicationName = Secrets.ApiKeys.GooglePlusApplicationName
            });
        }

        private string GetQueryTextForTopic(string topic)
        {
            string queryText = string.Empty;

            switch (topic)
            {
                case "f12": // F12
                    queryText = @"Internet-Explorer OR IE9 OR IE10 OR IE11 dev tools OR F12 OR developer OR chrome-devtools";
                    break;
                case "alt": // ALT
                    queryText = @"Visual-Studio ultimate OR debug OR lens OR sense OR map OR profiler OR intellitrace OR devops OR progression OR hub OR unit-tests";
                    break;
                case "xdt": // Xaml Design Tools
                    queryText = @"blend OR Visual-Studio OR microsoft OR unnir xaml";
                    break;
                case "hdt": // Html Design Tools
                    queryText = @"expression-blend OR visual-studio html OR css";
                    break;
                case "chakra": // Chakra runtime
                    queryText = @"javascript chakra OR v8 OR Internet-Explorer OR IE10 OR IE11 OR carakan OR tamarin OR monkey OR nitro OR jsrt";
                    break;
                case "jstools": // JavaScript tools
                    queryText = @"Visual-Studio javascript OR js";
                    break;
                case "typescript": // TypeScript
                    queryText = @"typescript";
                    break;
                case "cat": // CAT team
                    queryText = @"Visual-Studio azure OR packaging OR store OR phone OR emulator OR manifest OR publish";
                    break;
                case "visualstudio": // Visual Studio
                    queryText = @"""Visual Studio"" OR VS2012 OR VS2013";
                    break;
                case "appinsights": // Application Insights
                    queryText = @"""application insights"" OR ""app insights""";
                    break;
                case "fiddler": // Fiddler
                    queryText = @"fiddler browser OR network OR IE OR Chrome OR web OR request OR Firefox OR port OR mono OR mac OR xamarin OR linux OR telerik";
                    break;
                case "dev14": // Visual Studio 2015 (Dev14)
                    queryText = @"""visual studio"" OR vs AND 2015 ctp";
                    break;
            }

            return queryText;
        }
    }
}
