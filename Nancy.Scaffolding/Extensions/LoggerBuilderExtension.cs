using Serilog;
using Serilog.Builder;
using Serilog.Builder.Models;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Nancy.Scaffolding.Extensions
{
    public static class LoggerBuilderExtension
    {
        public static LoggerBuilder DisableConsoleIfConsoleSinkIsEnabled(this LoggerBuilder loggerBuilder, ConsoleOptions consoleOptions = null)
        {
            if (consoleOptions?.Enabled ?? false)
            {
                loggerBuilder.DisableConsole();
            }

            return loggerBuilder;
        }

        public static LoggerConfiguration EnableStdOutput(this LoggerConfiguration loggerConfiguration, ConsoleOptions consoleOptions = null)
        {
            if (consoleOptions?.Enabled ?? false)
            {
                var minimmumLevel = consoleOptions.MinimumLevel ?? LogEventLevel.Verbose;
                loggerConfiguration.WriteTo.Console(formatter: new JsonFormatter(), minimmumLevel).Enrich.FromLogContext();
            }

            return loggerConfiguration;
        }
    }
}
