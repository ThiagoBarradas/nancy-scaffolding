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

        public NewRelicOptions NewRelicOptions { get; set; } = new NewRelicOptions();

        public ConsoleOptions ConsoleOptions { get; set; } = new ConsoleOptions();
    }
}
