using Engine.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace Engine
{
    public class StackOverflowPostProvider : IPostProvider
    {
        public void Connect() { }

        public List<Post> GetPosts(TopicModel requestedTopic)
        {
            List<Post> posts = new List<Post>();
            var query = GetQueryTextForTopic(requestedTopic);
            var searchResults = getQueryResults(query);
            if (searchResults == null) return posts;

            foreach (var status in searchResults["items"])
            {
                // Convert Unix timestamp to DateTime
                var time = (double)status["creation_date"];
                var dt = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                dt = dt.AddSeconds(time);

                var id = getSubtitle(status);

                var p = new Post()
                {
                    ScreenName = (string)status["owner"]["display_name"],
                    Name = (string)status["owner"]["display_name"],
                    FollowersCount = (int)status["owner"]["reputation"],
                    ID = id,
                    UrlToPost = (string)status["link"],
                    UrlToUserProfile = (string)status["owner"]["link"],
                    UrlToUserAvatar = (string)status["owner"]["profile_image"],
                    DateCreated = dt,
                    SourceService = "stackoverflow",
                    Text = (string)status["title"]
                };

                posts.Add(p);
            };

            return posts;
        }

        private JObject getQueryResults(string query)
        {
            string json = string.Empty;
            JObject posts = null;

            using (var w = new WebClient())
            {
                try
                {
                    var client = new WebClient();
                    var responseStream = new GZipStream(client.OpenRead(query), CompressionMode.Decompress);
                    var reader = new StreamReader(responseStream);
                    json = reader.ReadToEnd();
                    posts = JObject.Parse(json);
                }
                catch (Exception) { return null; }
            }
            return posts;
        }

        private string GetQueryTextForTopic(TopicModel project)
        {
            string tagged = string.Empty;
            string intitle = string.Empty;
            string nottagged = string.Empty;

            return "https://api.stackexchange.com/2.2/search?" +
                  "key=" + Engine.Secrets.ApiKeys.StackOverflowAccessToken +
                  "&pagesize=20" +    // Only show last 20 posts. Should be plenty. 
                  "&order=desc" +
                  "&sort=creation" +
                  (project.StackOverflowTagged.Equals(string.Empty) ? string.Empty : "&tagged=" + project.StackOverflowTagged) +
                  (project.StackOverflowInTitle.Equals(string.Empty) ? string.Empty : "&intitle=" + project.StackOverflowInTitle) +
                  (project.StackOverflowNotTagged.Equals(string.Empty) ? string.Empty : "&nottagged=" + project.StackOverflowNotTagged) +
                  "&site=stackoverflow" +
                  "&filter=!9YdnSCK0n";
            // https://api.stackexchange.com/docs/search#order=desc&sort=activity&tagged=%5Binternet-explorer%5D&intitle=F12&filter=!9YdnSCK0n&site=stackoverflow&run=true
            // See above for filter details. 
        }

        // Creates the formatted reputation and badge display for SO posts. 
        public string getSubtitle(JToken status)
        {
            var ID = (status["owner"]["reputation"] ?? "") + "/";

            try
            {
                // SO doesn't always include the badges count
                ID = ID
                    + status["owner"]["badge_counts"]["bronze"] + "/"
                    + status["owner"]["badge_counts"]["silver"] + "/"
                    + status["owner"]["badge_counts"]["gold"];
            }
            catch (Exception)
            {
                // Catch all to show no badges. Is not displayed. 
                ID = ID + "0/0/0";
            }

            var split = ID.Split('/');
            var rep = split[0];
            var bronze = split[1];
            var silver = split[2];
            var gold = split[3];
            
            if (!string.IsNullOrWhiteSpace(rep))
            {
                // Reduce big numbers into xxx.xk.  Only a 1-2 char reduction, but follows SO format. 
                if (int.Parse(rep) > 1000)
                    rep = rep.Substring(0, rep.Length - 3) + "." + rep.Substring(rep.Length - 3, 1) + "k";
            }

            // Hacky insertion of numbers to make spans into circles. 
            return @"(<b>" + rep + "</b>) " +
                (gold.Equals("0") ? string.Empty : (@"<span class=""badge gold""></span>") + gold) +
                (silver.Equals("0") ? string.Empty : (@"<span class=""badge silver""></span>") + silver) +
                (bronze.Equals("0") ? string.Empty : (@"<span class=""badge bronze""></span>") + bronze);
        }
    }
}
