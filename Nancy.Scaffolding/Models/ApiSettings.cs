using Nancy.Scaffolding.Enums;

namespace Nancy.Scaffolding.Models
{
    public class ApiSettings
    {
        public string AppUrl { get; set; }

        public string PathPrefix { get; set; }

        public string Domain { get; set; }

        public string Application { get; set; }

        public string Version { get; set; }

        public JsonSerializerEnum JsonSerializer { get; set; }

        public string[] SupportedCultures { get; set; }

        public bool DebugMode { get; set; }
    }
}
