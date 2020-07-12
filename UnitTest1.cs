using NUnit.Framework;
using NUnit.Framework.Interfaces;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Appender;
using log4net.Core;
using System.Reflection;
using System;
using System.Threading;

namespace alza
{
    public class Tests
    {

        IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(Tests));

        [SetUp]
        public void Setup()
        {
            logSetup();

            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void Test1()
        {
            log.Info("Alza custom log by log4net");
            AlzaCareerPage careerPage = new AlzaCareerPage(driver);
            careerPage.goToPage();
            careerPage.clickOnIT();
            careerPage.searchByText("Quality");
            Thread.Sleep(2000);
            AlzaCareerPositionListPage positionListPage = careerPage.clickOn("Quality Assurance");
        }

        [TearDown]
        public void teardown(){
            log.Info("End of test");
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(@"Screenshot.jpg"); //+ TestContext.CurrentContext.Test.Name + ".jpg");
                log.Debug("Test result is not 'success', screenshot taken");
            }

            driver.Quit();
        }


        private void logSetup(){
            var hierarchy = (Hierarchy)LogManager.GetRepository(Assembly.GetEntryAssembly());

            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = "%level | %date | %message%newline"
            };
            patternLayout.ActivateOptions();

            var coloredConsoleAppender = new ConsoleAppender();
            coloredConsoleAppender.Layout = patternLayout;

            var rollingFileAppender = new RollingFileAppender
            {
                File = "logfile",
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = "yyyyMMdd-HHmm",
                Layout = patternLayout
            };

            hierarchy.Root.AddAppender(coloredConsoleAppender);
            // hierarchy.Root.AddAppender(rollingFileAppender);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

            BasicConfigurator.Configure(hierarchy);
        }
    }
}