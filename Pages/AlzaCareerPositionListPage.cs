using OpenQA.Selenium;
using log4net;
using System.Collections.ObjectModel;
using System.Threading;

namespace alza
{
    class AlzaCareerPositionListPage : PageObject
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AlzaCareerPositionListPage));
        public AlzaCareerPositionListPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement jobList()
        {
            return findElement(By.CssSelector("job-offer-list div.container a"));
        }

        private IWebElement headerTitle() {
            return findElement(By.CssSelector("job-detail-header h1"));
        }

        private IWebElement jobOffer(string name)
        {
            return findElement(By.XPath("//*[text()='" + name + "']"));
        }

        public void waitForJobListLoad()
        {
            jobList();
        }

        public string titleText() {
            return headerTitle().Text;
        }

        public ReadOnlyCollection<IWebElement> positionsList() {
            return findElements(By.CssSelector("h3.job-title"));
        }

        public AlzaPositionDetail goToPositionByName(string name) {
            log.Info("Try to find position by name " + name);
            jobOffer(name).Click();
            Thread.Sleep(2000);
            return new AlzaPositionDetail(driver);
        }
    }
}