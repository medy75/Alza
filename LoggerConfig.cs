using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Appender;
using log4net.Core;
using System.Reflection;

namespace alza
{
    class LoggerConfig
    {
        public static void logSetup(){
            var hierarchy = (Hierarchy)LogManager.GetRepository(Assembly.GetEntryAssembly());

            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = "%level | %date | %message%newline"
            };
            patternLayout.ActivateOptions();

            var consoleAppender = new ConsoleAppender();
            consoleAppender.Layout = patternLayout;

            hierarchy.Root.AddAppender(consoleAppender);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

            BasicConfigurator.Configure(hierarchy);
        }
    }
}