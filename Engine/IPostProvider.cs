using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine
{
    interface IPostProvider
    {
        void Connect();
        List<Post> GetPosts(string topic);
    }
}
