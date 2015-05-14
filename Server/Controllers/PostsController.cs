using Engine;
using Engine.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Server.Controllers
{
    public class PostsController : ApiController
    {
        List<TopicModel> _projectList;

        public PostsController()
        {
            _projectList = new List<TopicModel>()
            {
                new TopicModel() {
                    Name = "F12 Dev Tools", UrlSlug = "f12",
                    TwitterQuery = @"f12 OR tools OR debugging IE10 OR IE11 OR ""IE 11"" OR ""Internet Explorer""",
                    GooglePlusQuery = @"Internet-Explorer OR IE9 OR IE10 OR IE11 dev tools OR F12 OR developer OR chrome-devtools",
                    StackOverflowTagged = @"internet-explorer",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"f12",
                },
                new TopicModel() {
                    Name = "Visual Studio", UrlSlug = "visualstudio",
                    TwitterQuery = @"""visual studio"" OR VS2012 OR VS2013",
                    GooglePlusQuery = @"""Visual Studio"" OR VS2012 OR VS2013",
                    StackOverflowTagged = @"visual-studio;visual-studio-2013;visual-studio-2012;visual-studio-2015",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"",
                },
                new TopicModel() {
                    Name = "Visual Studio 2015", UrlSlug = "dev14",
                    TwitterQuery = @"""visual studio"" OR vs AND 2015 ctp",
                    GooglePlusQuery = @"""visual studio"" OR vs AND 2015 ctp",
                    StackOverflowTagged = @"visual-studio-2015",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"",
                },
                new TopicModel() {
                    Name = "Visual Studio Code", UrlSlug = "vscode",
                    TwitterQuery = @"""visual studio code"" OR ""vscode"" OR ""vs code"" OR @code",
                    GooglePlusQuery = @"visual-studio-code OR vscode OR vs-code",
                    StackOverflowTagged = @"vscode",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"vscode",
                },
                new TopicModel() {
                    Name = "Build 2015", UrlSlug = "build2015",
                    TwitterQuery = @"#build2015",
                    GooglePlusQuery = @"build2015",
                    StackOverflowTagged = @"",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"",
                },
                new TopicModel() {
                    Name = "Chakra JavaScript Runtime", UrlSlug = "chakra",
                    TwitterQuery = @"javascript chakra OR v8 OR ""Internet Explorer"" OR IE10 OR IE11 OR carakan OR tamarin OR monkey OR nitro OR jsrt",
                    GooglePlusQuery = @"javascript chakra OR v8 OR Internet-Explorer OR IE10 OR IE11 OR carakan OR tamarin OR monkey OR nitro OR jsrt",
                    StackOverflowTagged = @"internet-explorer",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = "javascript",
                },
                new TopicModel() {
                    Name = "TypeScript", UrlSlug = "typescript",
                    TwitterQuery = @"typescript",
                    GooglePlusQuery = @"typescript",
                    StackOverflowTagged = @"typescript",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"typescript",
                },
                new TopicModel() {
                    Name = "JavaScript Tools", UrlSlug = "jstools",
                    TwitterQuery = @"""Visual Studio"" javascript OR js",
                    GooglePlusQuery = @"Visual-Studio javascript OR js",
                    StackOverflowTagged = @"visual-studio",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"javascript",
                },
                new TopicModel() {
                    Name = "Application Insights", UrlSlug = "appinsights",
                    TwitterQuery = @"""application insights"" OR ""app insights""",
                    GooglePlusQuery = @"""application insights"" OR ""app insights""",
                    StackOverflowTagged = @"",
                    StackOverflowNotTagged = @"facebook",
                    StackOverflowInTitle = @"app insights",
                },
                new TopicModel() {
                    Name = "Visual Studio XAML Tools", UrlSlug = "xdt",
                    TwitterQuery = @"blend ""Visual Studio"" OR xaml OR microsoft OR unnir",
                    GooglePlusQuery = @"blend OR Visual-Studio OR microsoft OR unnir xaml",
                    StackOverflowTagged = @"visual-studio",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"xaml",
                },
                new TopicModel() {
                    Name = "Visual Studio HTML Design Tools", UrlSlug = "hdt",
                    TwitterQuery = @"blend ""Visual Studio"" OR html OR microsoft",
                    GooglePlusQuery = @"expression-blend OR visual-studio html OR css",
                    StackOverflowTagged = @"visual-studio",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"html",
                },
                new TopicModel() {
                    Name = "Fiddler", UrlSlug = "fiddler",
                    TwitterQuery = @"fiddler browser OR network OR IE OR Chrome OR web OR request OR Firefox OR port OR mono OR mac OR xamarin OR telerik",
                    GooglePlusQuery = @"fiddler browser OR network OR IE OR Chrome OR web OR request OR Firefox OR port OR mono OR mac OR xamarin OR linux OR telerik",
                    StackOverflowTagged = @"fiddler",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"fiddler",
                },
            };
        }

        // GET api/posts/f12
        public IEnumerable<Post> Get(string topic)
        {
            var pm = new PostsManager(_projectList);
            return pm.GetPosts(topic);
        }
    }
}