namespace Engine.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public string UrlSlug { get; set; }
                
        public string TwitterQuery { get; set; }
        
        public string GooglePlusQuery { get; set; }

        public string StackOverflowTagged { get; set; }

        public string StackOverflowNotTagged { get; set; }

        public string StackOverflowInTitle { get; set; }
    }
}
