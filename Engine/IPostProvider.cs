using Engine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engine
{
    interface IPostProvider
    {
        void Connect();
        List<Post> GetPosts(TopicModel requestedTopic);
    }
}
