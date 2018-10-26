using Swagger.ObjectModel;

namespace Nancy.Scaffolding.Docs
{
    public abstract class AbstractTagProvider
    {
        public string TagName { get; set; }

        public string TagDescription { get; set; }

        public AbstractTagProvider() { }

        public AbstractTagProvider(string name, string description)
        {
            this.TagName = name;
            this.TagDescription = description;
        }

        public Tag GetTag()
        {
            return new Tag()
            {
                Description = this.TagDescription,
                Name = this.TagName
            };
        }

        public string[] GetTags()
        {
            return new string[] { this.TagName };
        }
    }
}
