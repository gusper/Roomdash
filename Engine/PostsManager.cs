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

        public void Initialize()
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

            foreach (var providerList in _postProviders)
                posts.AddRange(providerList.GetPosts(project));

            return from post in posts
                   orderby post.DateCreated descending
                   select post;
        }
    }
}
