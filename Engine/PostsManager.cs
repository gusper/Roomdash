using Engine.Models;
using System.Collections.Generic;
using System.Linq;

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

        private void Initialize()
        {
            _postProviders = new List<IPostProvider>()
            {
                new TwitterPostProvider(),
                new GooglePlusPostProvider(),
                new StackOverflowPostProvider(),
                new RedditPostProvider(),
            };
            
            foreach (var provider in _postProviders)
            {
                provider.Connect();
            }
        }

        public IEnumerable<Post> GetPosts(string requestedTopic)
        {
            var posts = new List<Post>();

            Initialize();

            var requestedProject = _projects.Find(p => p.UrlSlug.ToLower() == requestedTopic.ToLower());

            foreach (var providerList in _postProviders)
                posts.AddRange(providerList.GetPosts(requestedProject));

            return from post in posts
                   orderby post.DateCreated descending
                   select post;
        }
    }
}
