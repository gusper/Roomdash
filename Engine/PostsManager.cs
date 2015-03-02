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
        private readonly List<IPostProvider> _postProviders = new List<IPostProvider>();
        private List<ProjectModel> _projects;

        public PostsManager(List<ProjectModel> projectList)
        {
            _projects = projectList;
        }

        private void Initialize(string project)
        {
            _postProviders.Add(new TwitterPostProvider());
            _postProviders.Add(new GooglePlusPostProvider());
            _postProviders.Add(new StackOverflowPostProvider());
            
            foreach (var provider in _postProviders)
            {
                provider.Connect();
            }
        }

        public IEnumerable<Post> GetPosts(string project)
        {
            var posts = new List<Post>();

            Initialize(project);
            foreach (var providerList in _postProviders)
                posts.AddRange(providerList.GetPosts(project));

            return from post in posts
                   orderby post.DateCreated descending
                   select post;
        }
    }
}
