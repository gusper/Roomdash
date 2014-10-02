using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using Engine.Models;
using Newtonsoft.Json.Linq;

namespace Engine
{
    public class StackOverflowPostProvider : IPostProvider
    {
        public void Connect()
        { }

        public List<Post> GetPosts(string topic)
        {
            List<Post> posts = new List<Post>();
            var query = GetQueryTextForTopic(topic);

            var searchResults = getQueryResults(query);

            foreach(var status in  searchResults["items"])
            {
                //Convert Unix timestamp to DateTime
                var time = (double)status["creation_date"];
                System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                dt = dt.AddSeconds(time);

                Post p = new Post(){
                    ScreenName = (string)status["owner"]["display_name"],
                    Name = (string)status["owner"]["display_name"],
                    FollowersCount = (int)status["owner"]["reputation"],
                    UrlToPost = (string)status["link"],
                    UrlToUserProfile = (string)status["owner"]["link"],
                    UrlToUserAvatar = (string)status["owner"]["profile_image"],
                    DateCreated = dt,
                    SourceService = "StackOverflow",
                    Text = (string)status["title"]
                };
                posts.Add(p);
            };
            return posts;
        }


        private JObject getQueryResults(string query)
        {
            var posts = new List<Post>();
            System.String json = string.Empty;

            using (var w = new WebClient())
            {
                try
                {
                    var client = new WebClient();
                    var responseStream = new GZipStream(client.OpenRead(query), CompressionMode.Decompress);
                    var reader = new StreamReader(responseStream);
                    json = reader.ReadToEnd();

                }
                catch (Exception) { }
            }
           return JObject.Parse(json);
        }

        private string GetQueryTextForTopic(string topic)
        {
            var query = "https://api.stackexchange.com/2.2/search?key=O3l2QcPi)Uvsohl4ojAKcA((&order=desc&sort=creation&tagged=internet-explorer&intitle=F12&site=stackoverflow";
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
            }

            return query;
        }
    }
}
