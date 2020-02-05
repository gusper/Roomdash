using Engine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engine
{
    interface IPostProvider
    {
        Task Connect();
        List<Post> GetPosts(TopicModel requestedTopic);
    }
}
