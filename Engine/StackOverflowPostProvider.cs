using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                var id = getSubtitle(status);

                Post p = new Post(){
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
            string tagged = string.Empty;
            string intitle = string.Empty;
            var nottagged = string.Empty;

            switch (topic)
            {
                    // query params chosen for what seemed to get the most hits. 
                    // tagged accepts OR in the form of tag;tag;tag
                    // intitle does not accept OR, all words must be present.
                    // nottagged filters out all posts with the provided tags.  Uses tag;tag;tag as OR.

                case "f12": // F12
                    tagged = "internet-explorer";
                    intitle = "F12";//@"f12 OR tools OR debugging IE10 OR IE11 OR ""IE 11"" OR ""Internet Explorer""";
                    break;
                case "alt": // ALT
                    tagged = "visual-studio"; //@"""Visual Studio"" ultimate OR debug OR lens OR sense OR map OR profiler OR intellitrace OR devops OR progression OR hub OR ""unit tests""";
                    intitle = "debug";
                    break;
                case "xdt": // Xaml Design Tools
                    tagged = "visual-studio"; //@"blend ""Visual Studio"" OR xaml OR microsoft OR unnir";
                    intitle = "xaml";
                    break;
                case "hdt": // Html Design Tools
                    tagged = "visual-studio"; //@"blend ""Visual Studio"" OR html OR microsoft";
                    intitle = "blend";
                    break;
                case "chakra": // Chakra runtime
                    tagged = "internet-explorer"; //@"javascript chakra OR v8 OR ""Internet Explorer"" OR IE10 OR IE11 OR carakan OR tamarin OR monkey OR nitro OR jsrt";
                    intitle = "chakra";
                    break;
                case "jstools": // JavaScript tools
                    tagged = "visual-studio"; // @"""Visual Studio"" javascript OR js";
                    intitle = "javascript";
                    break;
                case "typescript": // TypeScript
                    tagged = "typescript";
                    break;
                case "cat": // CAT team
                    tagged = "visual-studio";// azure OR packaging OR store OR phone OR emulator OR manifest OR publish";
                    intitle = "azure";
                    break;
                case "visualstudio": // Visual Studio
                    tagged = "visual-studio;visual-studio-2013;visual-studio-2012"; //@"""Visual Studio"" OR VS2012 OR VS2013";
                    break;
                case "appinsights": // Application Insights
                    intitle = "app insights";   //@"""application insights"" OR ""app insights""";
                    nottagged = "facebook";
                    break;
                case "fiddler": // Fiddler 
                    intitle = "fiddler";//@"fiddler browser OR network OR IE OR Chrome OR web OR request OR Firefox OR port OR mono OR mac OR xamarin OR telerik";
                    break;
            }
          return "https://api.stackexchange.com/2.2/search?" +
                "key=" + Engine.Secrets.ApiKeys.StackOverflowAccessToken +
                "&pagesize=20" +
                "&order=desc" +
                "&sort=creation" +
                (tagged.Equals(string.Empty) ? string.Empty : "&tagged=" + tagged) +
                (intitle.Equals(string.Empty) ? string.Empty : "&intitle=" + intitle) +
                (nottagged.Equals(string.Empty) ? string.Empty : "&nottagged=" + nottagged) +
                "&site=stackoverflow"+
                "&filter=!9YdnSCK0n";
            //https://api.stackexchange.com/docs/search#order=desc&sort=activity&tagged=%5Binternet-explorer%5D&intitle=F12&filter=!9YdnSCK0n&site=stackoverflow&run=true
            //See above for filter details. 
        }

        // Creates the formatted reputation and badge display for SO posts. 
        public string getSubtitle(JToken status)
        {
            var ID = status["owner"]["reputation"] + "/";
            try
            {
                ID = ID +    //SO doesn't always include the badges count, and doing LINQ null checks sucks. 
                         status["owner"]["badge_counts"]["bronze"] + "/"
                        + status["owner"]["badge_counts"]["silver"] + "/"
                        + status["owner"]["badge_counts"]["gold"];
            }
            catch (Exception)
            {
                ID = ID + "0/0/0";
            }
            var split = ID.Split('/');
            var rep = split[0];
            if(int.Parse(rep)>1000) rep = rep.Substring(0,rep.Length-3) + "." + rep.Substring(rep.Length-3,1) + "k";
            var bronze = split[1];
            var silver = split[2];
            var gold = split[3];

            return @"(<b>"+rep+"</b>) " +
                (bronze.Equals("0") ? string.Empty : bronze + @"<span class=""badge bronze"">&nbsp;7&nbsp;&nbsp;</span> ") +
                (silver.Equals("0") ? string.Empty : silver + @"<span class=""badge silver"">&nbsp;2&nbsp;&nbsp;</span> ") +
                (gold.Equals("0") ? string.Empty : gold + @"<span class=""badge gold"">&nbsp;&nbsp;1&nbsp;</span>");
        }

    }
}
