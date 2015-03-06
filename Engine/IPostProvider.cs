using Engine.Models;
using System.Collections.Generic;

namespace Engine
{
    interface IPostProvider
    {
        void Connect();
        List<Post> GetPosts(TopicModel requestedTopic);
    }
}
