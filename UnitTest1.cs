using NUnit.Framework;
using NUnit.Framework.Interfaces;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using log4net;
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
            LoggerConfig.logSetup();

            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        
        [Test]
        public void Test1()
        {
            log.Info("Start Test1");
            AlzaCareerPage careerPage = new AlzaCareerPage(driver);
            careerPage.goToPage();
            careerPage.clickOnIT();
            careerPage.searchByText("Quality");
            AlzaCareerPositionListPage positionListPage = careerPage.clickOn("Quality Assurance");
            positionListPage.waitForJobListLoad();
            Assert.AreEqual(positionListPage.titleText(), "Quality Assurance");
            AlzaPositionDetail positionDetailPage = positionListPage.goToPositionByName("Teamleader of QA Web Engineers");

            Assert.AreEqual(positionDetailPage.title(), "Teamleader of QA Web Engineers");
            Assert.AreEqual(positionDetailPage.firstPersonName(), "Ciencialová Barbora");
            Assert.AreEqual(positionDetailPage.secondPersonName(), "Tomusko Ján");

        }
        
        [Test]
        public void Test2()
        {
            log.Info("Start Test1");
            AlzaCareerPage careerPage = new AlzaCareerPage(driver);
            careerPage.goToPage();
            careerPage.clickOnIT();
            careerPage.searchByText("Quality");
            AlzaCareerPositionListPage positionListPage = careerPage.clickOn("Quality Assurance");
            positionListPage.waitForJobListLoad();
            Assert.AreEqual(positionListPage.titleText(), "Quality Assurance");
            AlzaPositionDetail positionDetailPage = positionListPage.goToPositionByName("Senior QA Engineer");

            Assert.AreEqual(positionDetailPage.title(), "Senior QA Engineer");
            Assert.AreEqual(positionDetailPage.firstPersonName(), "Ciencialová Barbora");
            Assert.AreEqual(positionDetailPage.secondPersonName(), "Tomusko Ján");

        }

        [TearDown]
        public void teardown(){
            log.Info("End of test");
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile("Screenshot.png");
                log.Debug("Test result is not 'success', screenshot taken ");
            }

            driver.Quit();
        }


        
    }
}