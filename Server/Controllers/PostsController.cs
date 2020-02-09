using Engine;
using Engine.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;

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
                    Name = "Visual Studio Preview", UrlSlug = "vspreview",
                    TwitterQuery = @"""visual studio"" preview",
                    StackOverflowTagged = @"",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"visual studio preview",
                    RedditQuery = @"visual studio preview",
                    RedditSubreddits = new List<string>() { "/r/visualstudio", "/r/programming" }
                },
                new TopicModel() {
                    Name = "Visual Studio", UrlSlug = "visualstudio",
                    TwitterQuery = @"""visual studio"" OR VS2015 OR VS2017 OR VS2019",
                    StackOverflowTagged = @"visual-studio;visual-studio-2013;visual-studio-2015;visual-studio-2017;visual-studio-2019",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"",
                    RedditQuery = @"",
                    RedditSubreddits = new List<string>() { "/r/visualstudio" }
                },
                new TopicModel() {
                    Name = "Visual Studio Code", UrlSlug = "vscode",
                    TwitterQuery = @"""visual studio code"" OR ""vscode"" OR ""vs code"" OR @code",
                    StackOverflowTagged = @"vscode",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"vscode",
                    RedditQuery = @"",
                    RedditSubreddits = new List<string>() { "/r/vscode" }
                },
                new TopicModel() {
                    Name = "TypeScript", UrlSlug = "typescript",
                    TwitterQuery = @"typescript",
                    StackOverflowTagged = @"typescript",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"typescript",
                    RedditQuery = @"",
                    RedditSubreddits = new List<string>() { }
                },
                new TopicModel() {
                    Name = "Fiddler", UrlSlug = "fiddler",
                    TwitterQuery = @"fiddler browser OR network OR IE OR Chrome OR web OR request OR Firefox OR port OR mono OR mac OR xamarin OR telerik",
                    StackOverflowTagged = @"fiddler",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"fiddler",
                    RedditQuery = @"",
                    RedditSubreddits = new List<string>() { }
                },
                new TopicModel() {
                    Name = "Azure API Apps", UrlSlug = "apiapps",
                    TwitterQuery = @"apiapps OR ""azure api app"" OR ""azure api apps""",
                    StackOverflowTagged = @"azure-api-apps",
                    StackOverflowNotTagged = @"",
                    StackOverflowInTitle = @"azure api app",
                    RedditQuery = @"",
                    RedditSubreddits = new List<string>() { }
                },
            };
        }

        // GET api/posts/f12
        public async Task<IEnumerable<Post>> GetAsync(string topic)
        {
            var pm = new PostsManager(_projectList);
            return await pm.GetPostsAsync(topic);
        }
    }   
}