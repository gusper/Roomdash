using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine
{
    public class PostsManager
    {
        private List<IPostProvider> _postProviders;
        private List<ProjectModel> _projects;

        public PostsManager(List<ProjectModel> projectList)
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
            };
            
            foreach (var provider in _postProviders)
            {
                provider.Connect();
            }
        }

        public IEnumerable<Post> GetPosts(string requestedProjectName)
        {
            var posts = new List<Post>();

            Initialize();

            var requestedProject = _projects.Find(p => p.UrlSlug.ToLower() == requestedProjectName.ToLower());

            foreach (var providerList in _postProviders)
                posts.AddRange(providerList.GetPosts(requestedProject));

            return from post in posts
                   orderby post.DateCreated descending
                   select post;
        }
    }
}
