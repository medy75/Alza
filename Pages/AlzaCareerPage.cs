using OpenQA.Selenium;
using log4net;
using System.Collections.ObjectModel;

namespace alza
{
    class AlzaCareerPage : PageObject
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AlzaCareerPage));

        public AlzaCareerPage(IWebDriver driver) : base(driver)
        {
        }


        private IWebElement ITradio() {
            return findElement(By.CssSelector("div.departments-list > div:nth-child(5)"));
        }

        private IWebElement searchField()
        {
            return findElement(By.Id("position"));
        }

        private ReadOnlyCollection<IWebElement> jobList()
        {
            return findElements(By.CssSelector("h3.job-title"));
        }

        public void goToPage() {
            string url = "https://www.alza.cz/kariera";
            goToUrl(url);
            searchField();
            log.Info("Go to " + url);
        }

        public void searchByText(string text) {
            searchField().SendKeys(text);
            log.Debug("Search by text: " + text);
        }

        public void clickOnIT() {
            ITradio().Click();
        }

        public AlzaCareerPositionListPage clickOn(string jobName){
            var list = jobList();
            foreach (IWebElement job in list)
            {
                if (jobName.Equals(job.Text))
                {
                    log.Info("Job with name " + jobName + " found. Try to click on it.");
                    job.Click();
                    return new AlzaCareerPositionListPage(driver);
                }
            }
            log.Info(jobName + " not found in list.");
            return null;
        }
    }
}