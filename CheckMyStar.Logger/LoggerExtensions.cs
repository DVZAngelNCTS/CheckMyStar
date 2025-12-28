using Microsoft.Extensions.Logging;

namespace CheckMyStar.Logger
{
    public static class LoggerExtensions
    {
        public static ILoggingBuilder AddLogger(this ILoggingBuilder builder)
        {
            builder
                .AddSimpleConsole(options =>
                {
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                });

            return builder;
        }
    }
}
