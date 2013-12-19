using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Post
    {
        public string ID { get; set; }
        public string SourceService { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public string ScreenName { get; set; }
        public string Name { get; set; }
        public string UrlToUserAvatar { get; set; }
        public string UrlToUserProfile { get; set; }
        public string UrlToPost { get; set; }
        public int FollowersCount { get; set; }
    }
}
