using Engine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engine
{
    interface IPostProvider
    {
        Task Connect();
        Task<List<Post>> GetPosts(TopicModel requestedTopic);
    }
}
