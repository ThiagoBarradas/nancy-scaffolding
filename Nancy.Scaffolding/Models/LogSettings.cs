using Serilog.Builder.Models;

namespace Nancy.Scaffolding.Models
{
    public class LogSettings
    {
        public string TitlePrefix { get; set; }

        public string[] JsonBlacklist { get; set; }
        
        public bool DebugEnabled { get; set; }

        public SeqOptions SeqOptions { get; set; } = new SeqOptions();

        public SplunkOptions SplunkOptions { get; set; } = new SplunkOptions();

        public GoogleCloudLoggingOptions GoogleCloudLoggingOptions { get; set; } = new GoogleCloudLoggingOptions();
    }
}
