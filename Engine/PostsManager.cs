using Engine.Models;
using LinqToTwitter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engine
{
    public class PostsManager
    {
        private List<IPostProvider> _postProviders;
        private List<TopicModel> _projects;

        public PostsManager(List<TopicModel> projectList)
        {
            _projects = projectList;
        }

        private async Task Initialize()
        {
            _postProviders = new List<IPostProvider>()
            {
                new TwitterPostProvider(),
                new StackOverflowPostProvider(),
                new RedditPostProvider(),
            };
            
            foreach (var provider in _postProviders)
            {
                await provider.Connect();
            }
        }

        public async Task<IEnumerable<Post>> GetPosts(string requestedTopic)
        {
            var posts = new List<Post>();

            await Initialize();

            var requestedProject = _projects.Find(p => p.UrlSlug.ToLower() == requestedTopic.ToLower());

            foreach (var providerList in _postProviders)
                posts.AddRange(await providerList.GetPosts(requestedProject));

            return from post in posts
                   orderby post.DateCreated descending
                   select post;
        }
    }
}
