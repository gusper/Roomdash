using Engine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engine
{
    interface IPostProvider
    {
        Task ConnectAsync();
        Task<List<Post>> GetPostsAsync(TopicModel requestedTopic);
    }
}
