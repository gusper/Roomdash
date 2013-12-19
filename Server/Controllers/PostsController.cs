using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Engine;
using Engine.Models;

namespace Server.Controllers
{
    public class PostsController : ApiController
    {
        // GET api/posts/f12
        public IEnumerable<Post> Get(string topic)
        {
            var pm = new PostsManager();
            pm.Initialize();
            return pm.GetPosts(topic);
        }
    }
}
